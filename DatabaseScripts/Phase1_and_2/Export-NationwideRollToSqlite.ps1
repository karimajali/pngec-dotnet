<#
.SYNOPSIS
    PNGEC Phase 1 & 2 Section 7: Nationwide Offline Voter Roll SQLite Builder
    Exports the complete national voter roll from SQL Server into an optimized SQLite database file,
    matching the Android Room schema, and generates a SHA-256 integrity checksum for offline distribution via USB OTG/SD card.

.PARAMETER DatabaseName
    SQL Server source database (defaults to 'pngec-staging')
.PARAMETER OutputPath
    Target output directory for the generated .db and .sha256 files
#>
param(
    [string]$ServerInstance = "localhost",
    [string]$DatabaseName = "pngec-staging",
    [string]$OutputDir = "$PSScriptRoot\OfflineRolls"
)

Write-Host "=================================================================" -ForegroundColor Cyan
Write-Host "  PNGEC NATIONWIDE OFFLINE VOTER ROLL BUILDER (SECTION 7)       " -ForegroundColor Yellow
Write-Host "=================================================================" -ForegroundColor Cyan

if (-not (Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir -Force | Out-Null
}

$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$dbFileName = "pngec_nationwide_roll_$timestamp.db"
$dbFilePath = Join-Path $OutputDir $dbFileName
$checksumFilePath = "$dbFilePath.sha256"
$csvTempPath = Join-Path $OutputDir "temp_roll_$timestamp.csv"

Write-Host "[1/4] Extracting voter records from SQL Server [$DatabaseName]..." -ForegroundColor Green

$sqlQuery = @"
SET NOCOUNT ON;
SELECT 
    ISNULL(ElectorId, 0) AS electorId,
    ISNULL(REPLACE(REPLACE(VoterName, char(10), ''), char(13), ''), '') AS voterName,
    ISNULL(REPLACE(REPLACE(LName, char(10), ''), char(13), ''), '') AS lName,
    ISNULL(REPLACE(REPLACE(FName, char(10), ''), char(13), ''), '') AS fName,
    ISNULL(Gender, '') AS gender,
    ISNULL(CONVERT(VARCHAR(20), DOB, 120), '') AS dob,
    ISNULL(ProvinceId, 0) AS provinceId,
    ISNULL(DistrictId, 0) AS districtId,
    ISNULL(LLGId, 0) AS llgId,
    ISNULL(WardId, 0) AS wardId,
    ISNULL(LocationId, 0) AS locationId,
    ISNULL(Status, 'OLD') AS status,
    ISNULL(SevisPassId, '') AS sevisPassId,
    ISNULL(SevisPassStatus, '') AS sevisPassStatus,
    ISNULL(UploadId, '') AS uploadId
FROM dbo.tblroll WITH (NOLOCK)
"@

sqlcmd -S $ServerInstance -E -d $DatabaseName -Q $sqlQuery -s "|" -W -h -1 -o $csvTempPath

if (-not (Test-Path $csvTempPath) -or (Get-Item $csvTempPath).Length -eq 0) {
    Write-Host "[!] Warning: No records found or extraction empty. Creating empty template." -ForegroundColor Yellow
}

Write-Host "[2/4] Initializing optimized SQLite schema matching Android Room..." -ForegroundColor Green

$schemaSql = @"
CREATE TABLE IF NOT EXISTS voterslist (
    electorId INTEGER PRIMARY KEY,
    tempElectorId INTEGER DEFAULT 0,
    oldElectorID INTEGER DEFAULT 0,
    voterName TEXT,
    lName TEXT,
    fName TEXT,
    gender TEXT,
    dob TEXT,
    provinceId INTEGER DEFAULT 0,
    districtId INTEGER DEFAULT 0,
    llgId INTEGER DEFAULT 0,
    wardId INTEGER DEFAULT 0,
    locationId INTEGER DEFAULT 0,
    status TEXT DEFAULT 'OLD',
    sevisPassId TEXT,
    sevisPassStatus TEXT,
    uploadId TEXT,
    isSyncToServer INTEGER DEFAULT 1,
    isDataUploaded INTEGER DEFAULT 1,
    isImageUploaded INTEGER DEFAULT 1
);

CREATE INDEX IF NOT EXISTS idx_voters_ward ON voterslist(wardId);
CREATE INDEX IF NOT EXISTS idx_voters_name ON voterslist(voterName);
CREATE INDEX IF NOT EXISTS idx_voters_sevispass ON voterslist(sevisPassId);
CREATE INDEX IF NOT EXISTS idx_voters_uploadid ON voterslist(uploadId);
"@

$schemaTempPath = Join-Path $OutputDir "temp_schema_$timestamp.sql"
Set-Content -Path $schemaTempPath -Value $schemaSql -Encoding UTF8
Get-Content $schemaTempPath | sqlite3 $dbFilePath
Remove-Item $schemaTempPath -Force

Write-Host "[3/4] Importing records into SQLite database..." -ForegroundColor Green

if (Test-Path $csvTempPath) {
    $records = Get-Content $csvTempPath
    $insertStatements = New-Object System.Text.StringBuilder
    [void]$insertStatements.AppendLine("BEGIN TRANSACTION;")

    foreach ($line in $records) {
        if ([string]::IsNullOrWhiteSpace($line)) { continue }
        $parts = $line.Split("|")
        if ($parts.Count -ge 12) {
            $electorId = $parts[0].Trim()
            if ($electorId -notmatch '^\d+$') { continue }
            $voterName = $parts[1].Replace("'", "''").Trim()
            $lName = $parts[2].Replace("'", "''").Trim()
            $fName = $parts[3].Replace("'", "''").Trim()
            $gender = $parts[4].Trim()
            $dob = $parts[5].Trim()
            $provinceId = [int]($parts[6].Trim())
            $districtId = [int]($parts[7].Trim())
            $llgId = [int]($parts[8].Trim())
            $wardId = [int]($parts[9].Trim())
            $locationId = [int]($parts[10].Trim())
            $status = $parts[11].Trim()
            $sevisPassId = if ($parts.Count -gt 12) { $parts[12].Trim() } else { "" }
            $sevisPassStatus = if ($parts.Count -gt 13) { $parts[13].Trim() } else { "" }
            $uploadId = if ($parts.Count -gt 14) { $parts[14].Trim() } else { "" }

            [void]$insertStatements.AppendLine("INSERT OR IGNORE INTO voterslist (electorId, voterName, lName, fName, gender, dob, provinceId, districtId, llgId, wardId, locationId, status, sevisPassId, sevisPassStatus, uploadId) VALUES ($electorId, '$voterName', '$lName', '$fName', '$gender', '$dob', $provinceId, $districtId, $llgId, $wardId, $locationId, '$status', '$sevisPassId', '$sevisPassStatus', '$uploadId');")
        }
    }
    [void]$insertStatements.AppendLine("COMMIT;")
    [void]$insertStatements.AppendLine("VACUUM;")

    $importTempPath = Join-Path $OutputDir "temp_import_$timestamp.sql"
    Set-Content -Path $importTempPath -Value $insertStatements.ToString() -Encoding UTF8
    Get-Content $importTempPath | sqlite3 $dbFilePath
    Remove-Item $importTempPath -Force
    Remove-Item $csvTempPath -Force
}

$countQuery = "SELECT COUNT(*) FROM voterslist;"
$recordCount = (sqlite3 $dbFilePath $countQuery)
if ($null -eq $recordCount) { $recordCount = "0" } else { $recordCount = $recordCount.ToString().Trim() }
$dbSizeMb = [math]::Round(((Get-Item $dbFilePath).Length / 1MB), 2)
Write-Host "      SQLite DB created successfully: $dbFileName ($dbSizeMb MB, $recordCount voters)" -ForegroundColor Cyan

Write-Host "[4/4] Generating cryptographic SHA-256 checksum..." -ForegroundColor Green
$sha256 = (Get-FileHash -Path $dbFilePath -Algorithm SHA256).Hash
Set-Content -Path $checksumFilePath -Value "$sha256 *$dbFileName"

Write-Host ""
Write-Host "=================================================================" -ForegroundColor Cyan
Write-Host "  NATIONWIDE OFFLINE ROLL GENERATION COMPLETE!                  " -ForegroundColor Green
Write-Host "=================================================================" -ForegroundColor Cyan
Write-Host "  Database File: $dbFilePath" -ForegroundColor White
Write-Host "  SHA256 File:   $checksumFilePath" -ForegroundColor White
Write-Host "  SHA-256 Hash:  $sha256" -ForegroundColor Yellow
Write-Host "  Total Records: $recordCount" -ForegroundColor Green
Write-Host "  Ready for deployment to tablet SD cards or USB OTG drives." -ForegroundColor Cyan
Write-Host "=================================================================" -ForegroundColor Cyan
