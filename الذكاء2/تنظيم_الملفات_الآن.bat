@echo off
echo 📁 تنظيم ملفات المشروع لـ GitHub...
echo.

REM إنشاء المجلدات الأساسية
echo 📂 إنشاء المجلدات...
mkdir data 2>nul
mkdir ml_models 2>nul
mkdir desktop_app 2>nul
mkdir tests 2>nul
mkdir docs 2>nul
mkdir reports 2>nul
mkdir scripts 2>nul
mkdir assets 2>nul
mkdir assets\screenshots 2>nul

REM نقل البيانات
echo 📊 نقل البيانات...
if exist "extracted1\StudentPerformance.csv" (
    copy "extracted1\StudentPerformance.csv" "data\" >nul
    echo ✅ تم نقل بيانات التدريب
)

REM نقل نماذج التعلم الآلي
echo 🧠 نقل نماذج التعلم الآلي...
if exist "linear_regression_analysis.py" (
    move "linear_regression_analysis.py" "ml_models\" >nul
    echo ✅ تم نقل ملف التحليل
)
if exist "linear_regression_report.py" (
    move "linear_regression_report.py" "ml_models\" >nul
    echo ✅ تم نقل ملف التقرير
)

REM نقل تطبيق سطح المكتب
echo 🖥️ نقل تطبيق سطح المكتب...
if exist "Program.cs" move "Program.cs" "desktop_app\" >nul
if exist "DatabaseManager.cs" move "DatabaseManager.cs" "desktop_app\" >nul
if exist "ModelTrainer.cs" move "ModelTrainer.cs" "desktop_app\" >nul
if exist "LinearRegressionModel.cs" move "LinearRegressionModel.cs" "desktop_app\" >nul
if exist "LoginForm.cs" move "LoginForm.cs" "desktop_app\" >nul
if exist "RegisterForm.cs" move "RegisterForm.cs" "desktop_app\" >nul
if exist "DashboardForm.cs" move "DashboardForm.cs" "desktop_app\" >nul
if exist "StudentPerformanceApp.csproj" move "StudentPerformanceApp.csproj" "desktop_app\" >nul
echo ✅ تم نقل ملفات C#

REM نقل الاختبارات
echo 🧪 نقل الاختبارات...
if exist "test_flask.py" (
    move "test_flask.py" "tests\" >nul
    echo ✅ تم نقل ملف الاختبارات
)

REM نقل التوثيق
echo 📚 نقل التوثيق...
if exist "DEPLOYMENT_GUIDE.md" move "DEPLOYMENT_GUIDE.md" "docs\" >nul
if exist "نشر_على_Heroku.md" move "نشر_على_Heroku.md" "docs\" >nul
if exist "نشر_على_Railway.md" move "نشر_على_Railway.md" "docs\" >nul
if exist "مقارنة_منصات_النشر.md" move "مقارنة_منصات_النشر.md" "docs\" >nul
if exist "نشر_Flask_الآن.md" move "نشر_Flask_الآن.md" "docs\" >nul
if exist "github_setup.md" move "github_setup.md" "docs\" >nul
if exist "README_FLASK.md" move "README_FLASK.md" "docs\" >nul
if exist "READY_TO_DEPLOY.md" move "READY_TO_DEPLOY.md" "docs\" >nul
if exist "DEPLOY_NOW.md" move "DEPLOY_NOW.md" "docs\" >nul
if exist "دليل_النشر_السريع.md" move "دليل_النشر_السريع.md" "docs\" >nul
if exist "README_DEPLOYMENT.md" move "README_DEPLOYMENT.md" "docs\" >nul
echo ✅ تم نقل ملفات التوثيق

REM نقل التقارير
echo 📋 نقل التقارير...
if exist "STATUS_REPORT.md" move "STATUS_REPORT.md" "reports\" >nul
if exist "FINAL_REPORT.md" move "FINAL_REPORT.md" "reports\" >nul
if exist "التقرير_النهائي_الشامل.md" move "التقرير_النهائي_الشامل.md" "reports\" >nul
if exist "تقرير_التطبيق.md" move "تقرير_التطبيق.md" "reports\" >nul
if exist "النتائج_النهائية.md" move "النتائج_النهائية.md" "reports\" >nul
echo ✅ تم نقل ملفات التقارير

REM نقل السكريبتات
echo 🔧 نقل السكريبتات...
if exist "run_app.bat" move "run_app.bat" "scripts\" >nul
if exist "create_zip.bat" move "create_zip.bat" "scripts\" >nul
move "تنظيم_الملفات_الآن.bat" "scripts\" >nul
echo ✅ تم نقل ملفات السكريبتات

REM نقل الموارد
echo 🖼️ نقل الموارد...
if exist "correlation_matrix.png" move "correlation_matrix.png" "assets\" >nul
if exist "linear_regression_results.png" move "linear_regression_results.png" "assets\" >nul
echo ✅ تم نقل ملفات الموارد

REM حذف الملفات غير المرغوبة
echo 🗑️ حذف الملفات غير المرغوبة...
if exist "users.db" del "users.db" >nul
if exist "*.pyc" del "*.pyc" >nul
if exist "archive (1).zip" del "archive (1).zip" >nul
if exist "app.py" del "app.py" >nul
if exist "flask_app.py" del "flask_app.py" >nul
if exist "الروابط.txt" del "الروابط.txt" >nul
rmdir /s /q "obj" 2>nul
rmdir /s /q "__pycache__" 2>nul
rmdir /s /q "extracted1" 2>nul
echo ✅ تم حذف الملفات غير المرغوبة

echo.
echo 🎉 تم تنظيم الملفات بنجاح!
echo.
echo 📋 الهيكل النهائي:
echo ├── 📄 README.md
echo ├── 📄 LICENSE  
echo ├── 📄 .gitignore
echo ├── 📄 flask_app_enhanced.py
echo ├── 📄 requirements.txt
echo ├── 📄 Procfile
echo ├── 📁 templates/
echo ├── 📁 data/
echo ├── 📁 ml_models/
echo ├── 📁 desktop_app/
echo ├── 📁 tests/
echo ├── 📁 docs/
echo ├── 📁 reports/
echo ├── 📁 scripts/
echo └── 📁 assets/
echo.
echo 🚀 المشروع جاهز للرفع على GitHub!
echo.
pause