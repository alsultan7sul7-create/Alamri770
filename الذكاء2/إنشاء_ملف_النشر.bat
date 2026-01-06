@echo off
chcp 65001 >nul
echo 📦 إنشاء ملف ZIP للنشر على Railway...
echo.

REM إنشاء مجلد مؤقت للملفات المطلوبة
mkdir railway_deploy 2>nul
cd railway_deploy

REM نسخ الملفات الأساسية المطلوبة للنشر
echo 📄 نسخ الملفات الأساسية...
copy "..\flask_app_enhanced.py" . >nul
copy "..\requirements.txt" . >nul
copy "..\Procfile" . >nul
copy "..\runtime.txt" . >nul
copy "..\app.json" . >nul
copy "..\README.md" . >nul

REM نسخ مجلد templates
echo 🎨 نسخ مجلد templates...
xcopy "..\templates" "templates\" /E /I /Q >nul

REM نسخ بيانات التدريب
echo 📊 نسخ بيانات التدريب...
mkdir data 2>nul
copy "..\extracted1\StudentPerformance.csv" "data\" >nul

REM إنشاء ملف ZIP
echo 📦 إنشاء ملف ZIP...
cd ..
powershell -command "Compress-Archive -Path 'railway_deploy\*' -DestinationPath 'railway_deployment.zip' -Force"

REM تنظيف المجلد المؤقت
rmdir /s /q railway_deploy

echo ✅ تم إنشاء ملف railway_deployment.zip بنجاح!
echo.
echo 📋 الملفات المضمنة:
echo ├── flask_app_enhanced.py
echo ├── requirements.txt
echo ├── Procfile
echo ├── runtime.txt
echo ├── app.json
echo ├── README.md
echo ├── templates/ (9 ملفات HTML)
echo └── data/StudentPerformance.csv
echo.
echo 🚄 الآن ارفع ملف railway_deployment.zip على Railway!
echo.
pause