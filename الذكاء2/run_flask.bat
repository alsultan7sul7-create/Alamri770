@echo off
echo ========================================
echo    نظام توقع أداء الطلاب - Flask
echo    Student Performance Prediction - Flask
echo ========================================
echo.

echo جاري اختبار النظام...
echo Testing system...
python test_flask.py

echo.
echo هل تريد تشغيل التطبيق؟ (y/n)
set /p choice=

if /i "%choice%"=="y" (
    echo.
    echo جاري تشغيل تطبيق Flask...
    echo Starting Flask application...
    echo.
    echo افتح المتصفح وانتقل إلى: http://localhost:5000
    echo Open browser and go to: http://localhost:5000
    echo.
    echo اضغط Ctrl+C لإيقاف التطبيق
    echo Press Ctrl+C to stop the application
    echo.
    python flask_app.py
) else (
    echo تم الإلغاء
    echo Cancelled
)

pause