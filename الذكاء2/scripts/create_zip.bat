@echo off
echo ========================================
echo    إنشاء ملف ZIP للمشروع
echo    Creating ZIP file for project
echo ========================================
echo.

set PROJECT_NAME=StudentPerformanceApp_%date:~-4,4%%date:~-10,2%%date:~-7,2%

echo جاري إنشاء ملف ZIP...
echo Creating ZIP file...

powershell Compress-Archive -Path "*.py","*.cs","*.csproj","*.md","*.txt","*.json","*.bat","templates","static","extracted1" -DestinationPath "%PROJECT_NAME%.zip" -Force

if exist "%PROJECT_NAME%.zip" (
    echo.
    echo ✅ تم إنشاء الملف بنجاح!
    echo ✅ File created successfully!
    echo.
    echo اسم الملف: %PROJECT_NAME%.zip
    echo File name: %PROJECT_NAME%.zip
    echo.
    echo يمكنك الآن مشاركة هذا الملف أو رفعه على:
    echo You can now share this file or upload it to:
    echo - GitHub
    echo - Google Drive  
    echo - OneDrive
    echo - Dropbox
    echo.
) else (
    echo ❌ فشل في إنشاء الملف
    echo ❌ Failed to create file
)

pause