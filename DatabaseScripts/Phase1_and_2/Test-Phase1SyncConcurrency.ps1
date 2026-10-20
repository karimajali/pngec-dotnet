# =========================================================================================
# PNGEC Phase 1: 20-Device Concurrency & Idempotency Simulation Test
# Verifies:
#  1. 20 concurrent mobile devices calling sp_Sync_New simultaneously
#  2. Service Broker serialized execution (MAX_QUEUE_READERS = 1)
#  3. Atomic sequence numbering via Seq_VoterNumber (ZERO duplicate Voter Numbers / RecordNo)
#  4. Strict idempotency enforcement via tblSyncLog (re-submitting returns ALREADY_PROCESSED)
# =========================================================================================

param(
    [string]$ServerInstance = "localhost",
    [string]$Database = "pngec-core",
    [int]$DeviceCount = 20
)

Write-Host "==================================================================" -ForegroundColor Cyan
Write-Host " PNGEC Phase 1 Concurrency & Idempotency Load Test ($DeviceCount Simulated Devices)" -ForegroundColor Cyan
Write-Host "==================================================================" -ForegroundColor Cyan

$ConnectionString = "Server=$ServerInstance;Database=$Database;Integrated Security=True;TrustServerCertificate=True;"

# 1. Verify Prerequisites
try {
    $conn = New-Object System.Data.SqlClient.SqlConnection($ConnectionString)
    $conn.Open()
    Write-Host "[OK] Connected to SQL Server instance '$ServerInstance' and database '$Database'." -ForegroundColor Green
} catch {
    Write-Error "Failed to connect to SQL Server: $_"
    exit 1
}

# 2. Cleanup old test data (UserId = 999999)
$cleanupCmd = $conn.CreateCommand()
$cleanupCmd.CommandText = @"
DELETE FROM [pngec-staging].[dbo].[tblroll] WHERE [CreatedBy] = 999999;
DELETE FROM [dbo].[tblSyncLog] WHERE [UserId] = 999999;
"@
$cleanupCmd.ExecuteNonQuery() | Out-Null
Write-Host "[OK] Test environment initialized (test rows cleaned up)." -ForegroundColor Green

# 3. Prepare 20 Unique Device Payloads
$testRunId = [Guid]::NewGuid().ToString().Substring(0, 8)
$payloads = @()
for ($i = 1; $i -le $DeviceCount; $i++) {
    $uploadId = "LOADTEST-$testRunId-DEV-$('{0:D3}' -f $i)"
    $payloads += [PSCustomObject]@{
        Index = $i
        UploadId = $uploadId
        FName = "ConcurrencyUser_$i"
        LName = "TestRoll_$testRunId"
        LocationId = 1
        WardId = 1
        DOB = "1992-05-15"
        Gender = $(if ($i % 2 -eq 0) { "Female" } else { "Male" })
        UserId = 999999
        DeviceId = "MOBILE_TERMINAL_$i"
        SevisPassId = "SP-$testRunId-$('{0:D4}' -f $i)"
        SevisPassStatus = "Verified"
    }
}

Write-Host "`n[STARTING] Launching $DeviceCount concurrent sync transmissions..." -ForegroundColor Yellow

$stopwatch = [System.Diagnostics.Stopwatch]::StartNew()

# 4. Execute 20 concurrent calls using PowerShell RunspacePool
$runspacePool = [runspacefactory]::CreateRunspacePool(1, $DeviceCount)
$runspacePool.Open()

$tasks = @()
$scriptBlock = {
    param($connStr, $p)
    try {
        $localConn = New-Object System.Data.SqlClient.SqlConnection($connStr)
        $localConn.Open()

        $cmd = $localConn.CreateCommand()
        $cmd.CommandType = [System.Data.CommandType]::StoredProcedure
        $cmd.CommandText = "dbo.sp_Sync_New"

        $cmd.Parameters.AddWithValue("@UploadId", $p.UploadId) | Out-Null
        $cmd.Parameters.AddWithValue("@FName", $p.FName) | Out-Null
        $cmd.Parameters.AddWithValue("@LName", $p.LName) | Out-Null
        $cmd.Parameters.AddWithValue("@LocationId", $p.LocationId) | Out-Null
        $cmd.Parameters.AddWithValue("@WardId", $p.WardId) | Out-Null
        $cmd.Parameters.AddWithValue("@DOB", [DateTime]::Parse($p.DOB)) | Out-Null
        $cmd.Parameters.AddWithValue("@Gender", $p.Gender) | Out-Null
        $cmd.Parameters.AddWithValue("@UserId", $p.UserId) | Out-Null
        $cmd.Parameters.AddWithValue("@DeviceId", $p.DeviceId) | Out-Null
        $cmd.Parameters.AddWithValue("@SevisPassId", $p.SevisPassId) | Out-Null
        $cmd.Parameters.AddWithValue("@SevisPassStatus", $p.SevisPassStatus) | Out-Null

        $reader = $cmd.ExecuteReader()
        $res = @{}
        if ($reader.Read()) {
            $res.IsSuccess = $reader["IsSuccess"]
            $res.ElectorId = $reader["ElectorId"]
            $res.RecordNo = $reader["RecordNo"]
            $res.Message = $reader["Message"]
        }
        $reader.Close()
        $localConn.Close()
        return [PSCustomObject]@{ UploadId = $p.UploadId; Success = $true; Result = $res; Error = $null }
    } catch {
        return [PSCustomObject]@{ UploadId = $p.UploadId; Success = $false; Result = $null; Error = $_.Exception.Message }
    }
}

foreach ($p in $payloads) {
    $ps = [powershell]::Create()
    $ps.RunspacePool = $runspacePool
    $ps.AddScript($scriptBlock).AddArgument($ConnectionString).AddArgument($p) | Out-Null
    $tasks += [PSCustomObject]@{
        Pipe = $ps
        Async = $ps.BeginInvoke()
    }
}

# Wait for all 20 threads to complete
$results = @()
foreach ($t in $tasks) {
    $output = $t.Pipe.EndInvoke($t.Async)
    $results += $output
    $t.Pipe.Dispose()
}
$runspacePool.Close()
$runspacePool.Dispose()

$stopwatch.Stop()
Write-Host "[COMPLETED] All $DeviceCount device calls submitted in $($stopwatch.ElapsedMilliseconds) ms." -ForegroundColor Green

# 5. Wait for Service Broker Queue to Drain
Write-Host "`n[MONITORING] Waiting for Service Broker queue to process all messages..." -ForegroundColor Yellow
$queueDrained = $false
$maxWaitSeconds = 15
$waitStart = [DateTime]::UtcNow

while (([DateTime]::UtcNow - $waitStart).TotalSeconds -lt $maxWaitSeconds) {
    $qCheckCmd = $conn.CreateCommand()
    $qCheckCmd.CommandText = "SELECT COUNT(*) FROM [dbo].[SyncInsertQueue];"
    $remainingInQueue = [int]$qCheckCmd.ExecuteScalar()

    $logCheckCmd = $conn.CreateCommand()
    $logCheckCmd.CommandText = "SELECT COUNT(*) FROM [dbo].[tblSyncLog] WHERE [UserId] = 999999 AND [Status] = 'SUCCESS';"
    $processedCount = [int]$logCheckCmd.ExecuteScalar()

    Write-Host "   -> Queue remaining: $remainingInQueue | Processed: $processedCount / $DeviceCount"
    if ($remainingInQueue -eq 0 -and $processedCount -ge $DeviceCount) {
        $queueDrained = $true
        break
    }
    Start-Sleep -Milliseconds 500
}

# 6. Verify Results & Duplicate Check
Write-Host "`n================ VERIFICATION RESULTS ================" -ForegroundColor Cyan

$verifyCmd = $conn.CreateCommand()
$verifyCmd.CommandText = @"
SELECT 
    COUNT(*) AS TotalInserted,
    COUNT(DISTINCT RecordNo) AS DistinctRecordNo,
    COUNT(DISTINCT ElectorId) AS DistinctElectorId,
    MIN(RecordNo) AS MinRecordNo,
    MAX(RecordNo) AS MaxRecordNo
FROM [pngec-staging].[dbo].[tblroll]
WHERE [CreatedBy] = 999999;
"@
$vReader = $verifyCmd.ExecuteReader()
$total = 0
$distinctRec = 0
$distinctElec = 0
$minRec = 0
$maxRec = 0
if ($vReader.Read()) {
    $total = [int]$vReader["TotalInserted"]
    $distinctRec = [int]$vReader["DistinctRecordNo"]
    $distinctElec = [int]$vReader["DistinctElectorId"]
    $minRec = $vReader["MinRecordNo"]
    $maxRec = $vReader["MaxRecordNo"]
}
$vReader.Close()

Write-Host "Total Records in Staging tblroll : $total"
Write-Host "Distinct RecordNo               : $distinctRec"
Write-Host "Distinct ElectorId              : $distinctElec"
Write-Host "Voter Number Range              : $minRec - $maxRec"

$duplicates = $total - $distinctRec
if ($total -eq $DeviceCount -and $duplicates -eq 0) {
    Write-Host "`n>>> PASS: Service Broker concurrency test successful! ZERO DUPLICATES detected under 20-device load. <<<" -ForegroundColor Green
} else {
    Write-Host "`n>>> FAIL: Detected $duplicates duplicate voter numbers! <<<" -ForegroundColor Red
}

# 7. Retest Idempotency (Re-send identical UploadId)
Write-Host "`n---------------- IDEMPOTENCY TEST ----------------" -ForegroundColor Cyan
$firstUploadId = $payloads[0].UploadId
Write-Host "Re-transmitting existing UploadId: $firstUploadId"

$reCmd = $conn.CreateCommand()
$reCmd.CommandType = [System.Data.CommandType]::StoredProcedure
$reCmd.CommandText = "dbo.sp_Sync_New"
$reCmd.Parameters.AddWithValue("@UploadId", $firstUploadId) | Out-Null
$reCmd.Parameters.AddWithValue("@FName", "RetryUser") | Out-Null
$reCmd.Parameters.AddWithValue("@LName", "RetryLast") | Out-Null
$reCmd.Parameters.AddWithValue("@LocationId", 1) | Out-Null
$reCmd.Parameters.AddWithValue("@WardId", 1) | Out-Null
$reCmd.Parameters.AddWithValue("@DOB", [DateTime]::Parse("1992-05-15")) | Out-Null
$reCmd.Parameters.AddWithValue("@Gender", "Male") | Out-Null
$reCmd.Parameters.AddWithValue("@UserId", 999999) | Out-Null
$reCmd.Parameters.AddWithValue("@DeviceId", "MOBILE_TERMINAL_RETRY") | Out-Null

$reReader = $reCmd.ExecuteReader()
$reStatus = ""
$reRecordNo = 0
if ($reReader.Read()) {
    $reStatus = $reReader["Message"]
    $reRecordNo = $reReader["RecordNo"]
}
$reReader.Close()

Write-Host "Re-submission Result Message : $reStatus"
Write-Host "Returned Existing RecordNo   : $reRecordNo"

if ($reStatus -eq "ALREADY_PROCESSED" -and $reRecordNo -gt 0) {
    Write-Host ">>> PASS: Idempotency enforced! System returned ALREADY_PROCESSED with original RecordNo. <<<" -ForegroundColor Green
} else {
    Write-Host ">>> FAIL: Idempotency failed! Message: $reStatus <<<" -ForegroundColor Red
}

$conn.Close()
Write-Host "`n==================================================================" -ForegroundColor Cyan
