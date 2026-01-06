@echo off
chcp 65001 >nul
color 0A
echo.
echo ╔══════════════════════════════════════════════════════════════╗
echo ║                   🚀 رفع المشروع على GitHub                    ║
echo ║                      دليل تفاعلي سهل                         ║
echo ╚══════════════════════════════════════════════════════════════╝
echo.

:check_git
echo 🔍 التحقق من وجود Git...
git --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ❌ Git غير مثبت على النظام
    echo.
    echo 📥 يرجى تثبيت Git أولاً:
    echo 1. اذهب إلى: https://git-scm.com/download/win
    echo 2. حمل وثبت Git for Windows
    echo 3. أعد تشغيل هذا الملف
    echo.
    pause
    start https://git-scm.com/download/win
    exit
) else (
    echo ✅ Git مثبت ومتاح
)

echo.
echo 📋 معلومات مطلوبة لرفع المشروع:
echo ═══════════════════════════════════════

:get_info
set /p github_username="🔹 أدخل اسم المستخدم على GitHub: "
if "%github_username%"=="" (
    echo ❌ يجب إدخال اسم المستخدم
    goto get_info
)

set /p user_name="🔹 أدخل اسمك الكامل: "
if "%user_name%"=="" set user_name=Developer

set /p user_email="🔹 أدخل بريدك الإلكتروني: "
if "%user_email%"=="" set user_email=developer@example.com

echo.
echo 📊 ملخص المعلومات:
echo ═══════════════════════
echo 👤 اسم المستخدم: %github_username%
echo 📧 البريد الإلكتروني: %user_email%
echo 📝 الاسم: %user_name%
echo 🔗 رابط Repository: https://github.com/%github_username%/student-performance-prediction
echo.

set /p confirm="هل المعلومات صحيحة؟ (y/n): "
if /i not "%confirm%"=="y" goto get_info

echo.
echo 🔧 إعداد Git...
echo ═══════════════════

git config --global user.name "%user_name%"
git config --global user.email "%user_email%"

echo ✅ تم إعداد Git بنجاح

echo.
echo 📁 إعداد Repository المحلي...
echo ═══════════════════════════════

if exist ".git" (
    echo ⚠️ Git repository موجود بالفعل
    set /p reinit="هل تريد إعادة الإعداد؟ (y/n): "
    if /i "%reinit%"=="y" (
        rmdir /s /q .git
        git init
        echo ✅ تم إعادة إنشاء Repository
    )
) else (
    git init
    echo ✅ تم إنشاء Git repository
)

echo.
echo 📄 إضافة الملفات...
git add .
if %errorlevel%==0 (
    echo ✅ تم إضافة جميع الملفات
) else (
    echo ❌ فشل في إضافة الملفات
    pause
    exit
)

echo.
echo 📊 حالة الملفات:
git status --short

echo.
echo 📝 إنشاء Commit...
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
    echo ✅ تم إنشاء Commit بنجاح
) else (
    echo ❌ فشل في إنشاء Commit
    pause
    exit
)

echo.
echo 🔗 ربط Repository بـ GitHub...
git remote remove origin >nul 2>&1
git remote add origin https://github.com/%github_username%/student-performance-prediction.git

if %errorlevel%==0 (
    echo ✅ تم ربط Repository بـ GitHub
) else (
    echo ❌ فشل في ربط Repository
    echo 💡 تأكد من إنشاء Repository على GitHub أولاً
    echo 🌐 اذهب إلى: https://github.com/new
    pause
    start https://github.com/new
    exit
)

echo.
echo 🚀 رفع الكود إلى GitHub...
echo ⏳ هذا قد يستغرق بضع دقائق...

git branch -M main
git push -u origin main

if %errorlevel%==0 (
    echo ✅ تم رفع الكود بنجاح!
) else (
    echo ❌ فشل في رفع الكود
    echo.
    echo 🔐 قد تحتاج إلى:
    echo 1. تسجيل الدخول إلى GitHub
    echo 2. استخدام Personal Access Token
    echo 3. التأكد من وجود Repository على GitHub
    echo.
    echo 💡 لإنشاء Personal Access Token:
    echo 1. اذهب إلى GitHub → Settings → Developer settings
    echo 2. Personal access tokens → Tokens (classic)
    echo 3. Generate new token
    echo 4. اختر صلاحيات repo
    echo.
    pause
    start https://github.com/settings/tokens
    exit
)

echo.
echo 🏷️ إنشاء Tag للإصدار...
git tag -a v2.0.0 -m "🎉 Version 2.0.0 - Enhanced AI System

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

git push origin --tags

if %errorlevel%==0 (
    echo ✅ تم إنشاء Tag ورفعه بنجاح
) else (
    echo ⚠️ تم رفع الكود لكن فشل في رفع Tags
)

echo.
echo ╔══════════════════════════════════════════════════════════════╗
echo ║                        🎉 تم بنجاح! 🎉                        ║
echo ╚══════════════════════════════════════════════════════════════╝
echo.
echo ✅ تم رفع مشروعك على GitHub بنجاح!
echo.
echo 🔗 روابط مهمة:
echo ═══════════════
echo 📁 Repository: https://github.com/%github_username%/student-performance-prediction
echo 📋 Issues: https://github.com/%github_username%/student-performance-prediction/issues
echo 🏷️ Releases: https://github.com/%github_username%/student-performance-prediction/releases
echo 📊 Insights: https://github.com/%github_username%/student-performance-prediction/pulse
echo.
echo 📱 شارك مشروعك:
echo ═══════════════════
echo 🎓 أطلقت للتو نظام توقع أداء الطلاب!
echo 🧠 يستخدم الذكاء الاصطناعي بدقة 98.90%%
echo 🌐 جربه الآن: https://github.com/%github_username%/student-performance-prediction
echo #ذكاء_اصطناعي #تعليم #تطوير_ويب #Flask #Python
echo.
echo 🚀 الخطوات التالية:
echo ═══════════════════
echo 1. 📸 أضف لقطات شاشة في assets/screenshots/
echo 2. 🌐 انشر التطبيق على Heroku أو Railway
echo 3. 📝 اكتب مقال عن مشروعك
echo 4. 💼 أضفه لسيرتك الذاتية
echo 5. 🌟 اطلب من الأصدقاء إعطاء نجمة للمشروع
echo.

set /p open_github="هل تريد فتح Repository على GitHub؟ (y/n): "
if /i "%open_github%"=="y" (
    start https://github.com/%github_username%/student-performance-prediction
)

echo.
echo 🙏 شكراً لاستخدام أداة الرفع السهلة!
echo 🌟 مشروعك الآن متاح للعالم!
echo.
pause