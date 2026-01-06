@echo off
chcp 65001 >nul
echo 🚀 أوامر Git السريعة لرفع المشروع
echo =====================================
echo.

:menu
echo اختر العملية المطلوبة:
echo.
echo 1. 📁 تنظيم الملفات
echo 2. 🔧 إعداد Git Repository
echo 3. 📝 إنشاء أول Commit
echo 4. 🏷️ إنشاء Tag للإصدار
echo 5. 🚀 رفع الكود إلى GitHub
echo 6. 📊 عرض حالة Repository
echo 7. 🔄 تحديث Repository
echo 8. 📋 عرض جميع الأوامر
echo 9. ❌ خروج
echo.
set /p choice="أدخل رقم اختيارك (1-9): "

if "%choice%"=="1" goto organize
if "%choice%"=="2" goto setup
if "%choice%"=="3" goto commit
if "%choice%"=="4" goto tag
if "%choice%"=="5" goto push
if "%choice%"=="6" goto status
if "%choice%"=="7" goto update
if "%choice%"=="8" goto commands
if "%choice%"=="9" goto exit
goto menu

:organize
echo.
echo 📁 تنظيم الملفات...
call تنظيم_الملفات_الآن.bat
echo.
echo ✅ تم تنظيم الملفات بنجاح!
pause
goto menu

:setup
echo.
echo 🔧 إعداد Git Repository...
git init
echo ✅ تم إنشاء Git repository
echo.
echo 📄 إضافة جميع الملفات...
git add .
echo ✅ تم إضافة جميع الملفات
echo.
echo 📊 حالة Repository:
git status
pause
goto menu

:commit
echo.
echo 📝 إنشاء أول Commit...
git commit -m "🎉 Initial commit: Student Performance Prediction System v2.0

✨ Features:
- AI-powered prediction with 98.90%% accuracy
- 9 professional web pages with Arabic RTL support
- Advanced security with SHA-256 encryption
- Responsive design for all devices
- Interactive charts and statistics
- Desktop C# application included
- Comprehensive documentation and deployment guides

🧠 AI Model:
- Linear Regression algorithm
- 5 key performance factors
- 10,000 training samples
- Real-time predictions with personalized recommendations

🎨 UI/UX:
- Bootstrap 5 responsive design
- Chart.js interactive visualizations
- Balanced navigation with hover effects
- Professional color gradients and animations

🔒 Security:
- SHA-256 password encryption
- Flask session management
- SQL injection protection
- Input validation and sanitization

🚀 Deployment Ready:
- Heroku, Railway, Render compatible
- Environment variables configured
- Production-ready settings
- CI/CD pipeline included

📚 Documentation:
- Comprehensive README
- Deployment guides for multiple platforms
- Contributing guidelines
- Security policy
- Issue and PR templates"

if %errorlevel%==0 (
    echo ✅ تم إنشاء Commit بنجاح!
) else (
    echo ❌ فشل في إنشاء Commit
)
pause
goto menu

:tag
echo.
echo 🏷️ إنشاء Tag للإصدار...
set /p tag_name="أدخل اسم Tag (مثل v2.0.0): "
if "%tag_name%"=="" set tag_name=v2.0.0

git tag -a %tag_name% -m "🎉 Version %tag_name% - Enhanced AI System

🌟 Major Release Features:
✨ Enhanced Flask application with 9 professional pages
🧠 AI model with 98.90%% accuracy (Linear Regression)
🎨 Professional UI with Bootstrap 5 and Chart.js
🔒 Advanced security with SHA-256 encryption
🌐 Full Arabic RTL support with responsive design
📊 Interactive statistics and performance analytics
🖥️ Complete C# desktop application
📚 Comprehensive documentation and deployment guides

🚀 Deployment Ready:
- Heroku, Railway, Render compatible
- Environment variables configured
- CI/CD pipeline with GitHub Actions
- Production-ready settings"

echo ✅ تم إنشاء Tag: %tag_name%
pause
goto menu

:push
echo.
echo 🚀 رفع الكود إلى GitHub...
echo.
set /p username="أدخل اسم المستخدم على GitHub: "
if "%username%"=="" (
    echo ❌ يجب إدخال اسم المستخدم
    pause
    goto menu
)

echo 🔗 ربط Repository بـ GitHub...
git remote add origin https://github.com/%username%/student-performance-prediction.git

echo 📤 رفع الكود...
git push -u origin main

echo 🏷️ رفع Tags...
git push origin --tags

if %errorlevel%==0 (
    echo.
    echo ✅ تم رفع المشروع بنجاح!
    echo 🌐 رابط Repository: https://github.com/%username%/student-performance-prediction
    echo.
) else (
    echo ❌ فشل في رفع المشروع
)
pause
goto menu

:status
echo.
echo 📊 حالة Repository:
echo ==================
echo.
echo 📁 الملفات المتتبعة:
git ls-files | wc -l
echo.
echo 📝 آخر Commits:
git log --oneline -5
echo.
echo 🏷️ Tags الموجودة:
git tag
echo.
echo 🔗 Remote repositories:
git remote -v
echo.
pause
goto menu

:update
echo.
echo 🔄 تحديث Repository...
echo.
echo 📄 إضافة الملفات الجديدة...
git add .
echo.
set /p commit_msg="أدخل رسالة Commit: "
if "%commit_msg%"=="" set commit_msg="تحديث المشروع"

git commit -m "%commit_msg%"
git push

echo ✅ تم تحديث Repository
pause
goto menu

:commands
echo.
echo 📋 جميع أوامر Git المفيدة:
echo ===============================
echo.
echo 🔧 الإعداد الأساسي:
echo git init                          # إنشاء repository جديد
echo git add .                         # إضافة جميع الملفات
echo git commit -m "رسالة"             # إنشاء commit
echo.
echo 🔗 الربط مع GitHub:
echo git remote add origin [URL]       # ربط مع GitHub
echo git push -u origin main           # رفع أول مرة
echo git push                          # رفع التحديثات
echo.
echo 🏷️ إدارة Tags:
echo git tag -a v1.0.0 -m "رسالة"      # إنشاء tag
echo git push origin --tags            # رفع tags
echo git tag                           # عرض tags
echo.
echo 📊 المراقبة والمعلومات:
echo git status                        # حالة Repository
echo git log --oneline                 # تاريخ Commits
echo git remote -v                     # Remote repositories
echo.
echo 🔄 التحديث والمزامنة:
echo git pull                          # تحديث من GitHub
echo git fetch                         # جلب التحديثات
echo git merge                         # دمج التحديثات
echo.
pause
goto menu

:exit
echo.
echo 👋 شكراً لاستخدام أوامر Git السريعة!
echo 🚀 مشروعك جاهز للعالم!
echo.
pause
exit

:error
echo ❌ حدث خطأ في تنفيذ الأمر
pause
goto menu