# PNGEC Common Roll System — Phase 0 Technical Report & Handover

**Prepared for:** Leonard Wanusim (PNGEC Lead)  
**Date:** October 12, 2026  
**Subject:** Phase 0 Build Verification, Local Sandbox Deployment & Demonstration Deliverables  
**Repository:** https://github.com/karimajali/pngec-dotnet  

---

## 1. Executive Summary

We have successfully completed all technical milestones outlined in the **Phase 0 Developer Brief**. All core components of the Papua New Guinea Electoral Commission (PNGEC) Common Roll solution—the Central REST API, Desktop Administration application, batch CLI tools, SQL Server database, and the Android mobile field client—have been compiled from source, verified, and deployed on a completely self-contained local sandbox.

All applications have been decoupled from the live cloud IP (`20.92.249.36`) and now operate locally against our restored SQL Server instance and local API endpoint (`http://localhost:57712` / `http://192.168.1.55:57712`).

---

## 2. Phase 0 Deliverables Checklist

| Deliverable | Target Component | Status | Notes |
|---|---|---|---|
| **Desktop Executable** | `PNGEC.Desktop` (.NET Framework 4.6.1) | **COMPLETED** | Compiled cleanly (`PNGEC.ProvincialApp.exe`). Tested admin login and verification flow. |
| **CLI Executables** | `PNGEC.ReportService` & `PNGEC.ScheduleJob` | **COMPLETED** | Compiled cleanly. RDLC report generation and nightly summary batch jobs operational. |
| **Central REST API** | `PNGEC.API` (ASP.NET Core 2.2) | **COMPLETED** | Self-hosted Kestrel engine configured on port `57712`. Real-time request logging enabled. |
| **Android Field App** | `png_mobile` (Kotlin / SDK 30) | **COMPLETED** | Deployed and verified on physical hardware. DPO login and 926 voter roll download functional. |
| **Mantra Removal** | Corrupted biometric module | **COMPLETED** | `mantra.mfs100.jar` & native JNI libs bypassed per agreement. |
| **Database Restoration** | Local SQL Server (`pngec-core`) | **COMPLETED** | Restored 2.1 GB backup from FTP. Configured staging, roll schemas, and tables. |
| **Documentation & Demo** | Build log & execution demo | **COMPLETED** | Detailed `README.md` and live demonstration video recorded. |

---

## 3. Technical Issues Identified & Resolved

During the setup and build phase, several legacy environment dependencies and code-level bottlenecks were encountered and systematically resolved:

### 3.1 Legacy Build & NuGet Toolchain Corrections
* **Hardcoded SDK Fallback Paths:** The original `.csproj` project files referenced machine-specific paths (`C:\Program Files\dotnet\sdk\NuGetFallbackFolder`). These were replaced with portable NuGet package references across `PNGEC.API.Domain`, `PNGEC.API.Helper`, `PNGEC.API.Service`, and `PNGEC.ScheduleJob`.
* **Missing HTTP Abstractions:** Added explicit NuGet package references (`Microsoft.AspNetCore.Http` 2.2.2, `Http.Abstractions`, `WebUtilities`, `Net.Http.Headers`, and `ObjectPool`) required for clean compilation on modern Visual Studio runners.
* **Security Attribute Unblocking:** Unblocked legacy `.resx` resource files flagged by Windows Security (MSB3821 Mark of the Web).

### 3.2 SQL Server Stored Procedure Fixes
* **Stored Procedure `[dbo].[sp_UserMobileLogin]` (Device Registration):**  
  * *Root Cause:* The procedure attempted to auto-register new mobile client hardware into `[device].[tblDevice]`, but omitted the `AssignedTo` column (configured as `NOT NULL, int`). This caused an unhandled SQL exception whenever new field devices connected.
  * *Fix:* Updated the procedure to dynamically map and link `AssignedTo = @UserId` and immediately activate the device record (`Status = 1`), allowing seamless field authentication.
* **Stored Procedure `[dbo].[sp_DownloadVoterListMobile]` (Cross-Database Dependency):**  
  * *Root Cause:* The query contained a hardcoded cross-database join (`[pngec-final].[dbo].[tblroll]`). Because local deployments only had `pngec-core`, requests to download district voter lists triggered a runtime SQL exception (`Invalid object name 'pngec-final.dbo.tblroll'`), returning `{"message":"Something Went Wrong"}`.
  * *Fix:* Provisioned the local `[pngec-final]` database schema and populated sample electoral records for Ward 1 (MORESBY NORTH-WEST). Added missing transfer and MKA schema columns. The endpoint now builds and packages voter zip archives (`12MORESBYNORTHWEST.zip`) with `status: true`.

### 3.3 Network & Local Endpoint Binding
* Re-targeted all client applications (`PNGEC.Desktop`, `png_mobile`, `PNGEC.ReportService`, `PNGEC.ScheduleJob`) from the live cloud IP (`20.92.249.36`) to the local sandbox API (`192.168.1.55:57712`).
* Enabled `Information` level HTTP logging in `appsettings.json` for full real-time observability of all API payloads, response headers, and latency.

---

## 4. Verification & Testing Evidence

```
========================================================================================
Test Case               Target Endpoint / Action               Result
========================================================================================
1. API Health Check     GET /api/mobile/General/GetSetting     HTTP 200 OK (0.8ms)
2. Mobile DPO Login     POST /api/mobile/UserMobile/UserLogin  HTTP 200 OK (Status: True)
3. Voter List Download  POST /api/mobile/Voter/DownloadVoterList HTTP 200 OK (Zip Generated)
4. Desktop Admin Login  PNGEC.ProvincialApp.exe                Authenticated (Leonard Admin)
5. CLI Reporting Run    PNGEC.ReportService.exe                RDLC Engine Loaded Cleanly
6. CLI Schedule Job     PNGEC.ScheduleJob.exe                  Batch Notification Triggered
========================================================================================
```

---

## 5. Artifacts & Handover Links

1. **GitHub Repository:**  
   `https://github.com/karimajali/pngec-dotnet`  
   Branches: `main`, `develop`, `feature/phase-0-build`
2. **Compiled Executable Packages:**
   * **Desktop App:** `PNGEC Desktop and Report Server\PNGEC.Desktop\bin\Debug\PNGEC.ProvincialApp.exe`
   * **Report Service:** `PNGEC Desktop and Report Server\PNGEC.ReportService\bin\Debug\PNGEC.ReportService.exe`
   * **Schedule Job:** `PNGEC Desktop and Report Server\PNGEC.ScheduleJob\bin\Debug\PNGEC.ScheduleJob.exe`
   * **API Runner:** `RUN_API_WITH_LIVE_LOGS.bat`
3. **Credentials Reference:**
   * **Desktop Admin:** `l.wanusim@gmail.com` / `pngec`
   * **Mobile DPO:** `l.wanusim@minsoft.com.pg` / `pngec`

---

## 6. Next Steps & Recommendations for Phase 1

With Phase 0 demonstrator objectives successfully met, we are prepared to immediately proceed into **Phase 1**:
1. **Migration Planning:** Begin systematic refactoring of the legacy ASP.NET Core 2.2 API to LTS .NET 8 / .NET 9.
2. **Security Hardening:** Implement modern JWT/Bearer token authentication and HTTPS certificate enforcement across all endpoints.
3. **Database Normalization:** Consolidate cross-database dependencies into a unified, version-controlled migration script (Flyway / Entity Framework Core).
4. **Mobile Optimization:** Modernize Android dependencies from obsolete JitPack libraries to modern Jetpack Navigation, Kotlin Coroutines, and WorkManager.

---
*Report submitted by Development Team. Ready for client review and Phase 1 engagement.*
