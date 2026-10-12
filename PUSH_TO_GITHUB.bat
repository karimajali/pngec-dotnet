@echo off
title Push PNGEC .NET Code to GitHub
echo ========================================================
echo   Pushing PNGEC Codebase to GitHub (main, develop, feature)
echo ========================================================
echo.
cd /d "C:\Users\other\Desktop\C# Project\PNGEC Desktop and Report Server"

echo [1/4] Committing staged files...
git commit -m "Phase 0: Build demo - PNGEC Desktop, API, ReportService, and ScheduleJob"

echo.
echo [2/4] Pushing to main branch...
git branch -M main
git push -u origin main

echo.
echo [3/4] Creating and pushing develop branch...
git checkout -b develop
git push -u origin develop

echo.
echo [4/4] Creating and pushing feature/phase-0-build branch...
git checkout -b feature/phase-0-build
git push -u origin feature/phase-0-build

echo.
echo ========================================================
echo   ALL BRANCHES PUSHED SUCCESSFULLY!
echo ========================================================
pause
