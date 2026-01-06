@echo off
echo ========================================
echo       تحضير ملفات النشر على Render
echo ========================================
echo.

echo 📁 إنشاء مجلد render_files...
if exist render_files rmdir /s /q render_files
mkdir render_files

echo 📋 نسخ الملفات الأساسية...
copy flask_app_enhanced.py render_files\
copy requirements.txt render_files\
copy render.yaml render_files\
copy Procfile render_files\
copy StudentPerformance.csv render_files\

echo 📁 نسخ المجلدات...
xcopy templates render_files\templates\ /E /I /Q
xcopy static render_files\static\ /E /I /Q
xcopy data render_files\data\ /E /I /Q

echo.
echo ✅ تم تحضير الملفات في مجلد render_files
echo.
echo 📋 الملفات المحضرة:
dir render_files /B

echo.
echo 🚀 الخطوات التالية:
echo 1. اضغط أي مفتاح لضغط الملفات في ZIP
echo 2. ارفع الملف المضغوط على GitHub
echo 3. اربط GitHub بـ Render
echo.
pause

echo 📦 إنشاء ملف مضغوط...
powershell -command "Compress-Archive -Path 'render_files\*' -DestinationPath 'render_ready.zip' -Force"

if exist render_ready.zip (
    echo ✅ تم إنشاء render_ready.zip بنجاح!
    echo.
    echo 🎯 الآن:
    echo 1. ارفع render_ready.zip على GitHub
    echo 2. أو استخرج محتوياته ورفعها منفردة
    echo 3. اربط repository بـ Render
) else (
    echo ❌ فشل في إنشاء الملف المضغوط
    echo يمكنك نسخ ملفات render_files يدوياً
)

echo.
echo 📞 تحتاج مساعدة؟ أخبرني!
pause