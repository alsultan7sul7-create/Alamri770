@echo off
echo ========================================
echo    نظام توقع أداء الطلاب
echo    Student Performance Prediction System
echo ========================================
echo.

echo جاري التحقق من متطلبات النظام...
echo Checking system requirements...

:: التحقق من وجود .NET
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo خطأ: .NET غير مثبت على النظام
    echo Error: .NET is not installed
    echo يرجى تحميل وتثبيت .NET 6.0 أو أحدث من:
    echo Please download and install .NET 6.0 or later from:
    echo https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

echo تم العثور على .NET
echo .NET found successfully

echo.
echo جاري بناء المشروع...
echo Building project...

:: بناء المشروع
dotnet build --configuration Release
if %errorlevel% neq 0 (
    echo خطأ في بناء المشروع
    echo Build failed
    pause
    exit /b 1
)

echo تم بناء المشروع بنجاح
echo Project built successfully

echo.
echo جاري تشغيل التطبيق...
echo Starting application...

:: تشغيل التطبيق
dotnet run --configuration Release

echo.
echo تم إغلاق التطبيق
echo Application closed
pause