# PNGEC Common Roll — .NET Applications & API Server

This repository contains the backend API, desktop management application, and CLI tools for the Papua New Guinea Electoral Commission (PNGEC) Common Roll system.

## Components

| Component | Type | Framework | Description |
|---|---|---|---|
| **PNGEC.Desktop** | WinForms Application | .NET Framework 4.6.1 | Provincial Admin Desktop application for voter registration, preliminary/final roll approvals, and reporting. |
| **PNGEC.API** | REST API | ASP.NET Core 2.2 | Central REST API serving both Desktop and Android mobile field units. Talks to SQL Server via stored procedures. |
| **PNGEC.ReportService** | Console (CLI) | .NET Framework 4.6.1 | Batch reporting tool utilizing RDLC reports and ZXing. |
| **PNGEC.ScheduleJob** | Console (CLI) | .NET Framework 4.6.1 | Nightly summary emailing service. |
| **API Class Libraries** | Class Libraries | .NET Framework 4.6.1 | `PNGEC.API.Domain`, `PNGEC.API.Helper`, `PNGEC.API.Repository`, `PNGEC.API.Service` (Dapper calling stored procedures). |

---

## Changes Made for Clean Build (Phase 0)

1. **Fixed Hardcoded NuGetFallbackFolder References**:
   - Removed legacy hardcoded paths pointing to `C:\Program Files\dotnet\sdk\NuGetFallbackFolder\...` in:
     - `PNGEC.API.Domain/PNGEC.API.Domain.csproj`
     - `PNGEC.API.Helper/PNGEC.API.Helper.csproj`
     - `PNGEC.API.Service/PNGEC.API.Service.csproj`
     - `PNGEC.ScheduleJob/PNGEC.ScheduleJob.csproj`
   - Replaced with standard packages from NuGet repository.

2. **Resolved Missing NuGet Package References**:
   - Added explicit references to:
     - `Microsoft.AspNetCore.Http` (2.2.2)
     - `Microsoft.AspNetCore.Http.Abstractions` (2.2.0)
     - `Microsoft.AspNetCore.WebUtilities` (2.2.0)
     - `Microsoft.Net.Http.Headers` (2.2.0)
     - `Microsoft.Extensions.ObjectPool` (2.2.0)
     - `System.Text.Encodings.Web` (4.5.0)
     - `System.ComponentModel.Annotations` (4.5.0)

3. **Resolved Resource Locking (MSB3821)**:
   - Unblocked downloaded `.resx` files (Mark of the Web security flag).

4. **Database Stored Procedure Fix**:
   - Resolved issue in `[dbo].[sp_UserMobileLogin]` where new device auto-registration failed due to `AssignedTo` not allowing nulls in `[device].[tblDevice]`. Stored procedure now dynamically links and activates connecting devices.

---

## Prerequisites

- **Visual Studio 2019 or 2022** with:
  - .NET desktop development workload (.NET Framework 4.6.1 Developer Pack)
  - ASP.NET and web development
- **.NET Core 2.2 Runtime** (for running `PNGEC.API`)
- **SQL Server 2017+** with `pngec-core` database restored

---

## Build Instructions

### Via Command Line (MSBuild)
```powershell
# Restore NuGet packages
nuget restore PNGEC.Desktop.sln

# Build all projects in Debug configuration
msbuild PNGEC.Desktop.sln /p:Configuration=Debug /p:Platform="Any CPU" /m
```

### Via Visual Studio
1. Open `PNGEC.Desktop.sln` in Visual Studio.
2. Select **Build > Build Solution** (or press `Ctrl + Shift + B`).

---

## Running the Applications

### 1. Database Configuration
Ensure SQL Server is running and database `pngec-core` is restored.
Update connection string in `PNGEC.API/appsettings.json` if necessary:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=localhost;Initial Catalog=pngec-core;Integrated Security=True;TrustServerCertificate=True;"
}
```

### 2. Running PNGEC.API
From the solution root:
```powershell
cd "PNGEC.API\bin\Debug\netcoreapp2.2"
dotnet PNGEC.API.dll --urls "http://0.0.0.0:57712"
```

### 3. Running PNGEC.Desktop
Run the compiled executable located at:
```
PNGEC.Desktop\bin\Debug\PNGEC.ProvincialApp.exe
```

Default Admin Credentials:
- **Email:** `l.wanusim@gmail.com`
- **Password:** `pngec`

---

## Security & Confidentiality Notice
Per project confidentiality guidelines:
- Never commit actual voter roll data, exports, or `uploads/`.
- Connection strings and credentials in version control should target local development instances only.
