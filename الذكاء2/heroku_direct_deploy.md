# 🚀 النشر المباشر على Heroku

## الطريقة السريعة:

### 1. تثبيت Heroku CLI
- Windows: تحميل من https://devcenter.heroku.com/articles/heroku-cli
- أو استخدام: `winget install Heroku.CLI`

### 2. تسجيل الدخول
```bash
heroku login
```

### 3. إنشاء تطبيق
```bash
heroku create student-performance-app-2026
```

### 4. رفع المشروع
```bash
git init
git add .
git commit -m "Deploy to Heroku"
heroku git:remote -a student-performance-app-2026
git push heroku main
```

### 5. فتح التطبيق
```bash
heroku open
```

## الرابط النهائي سيكون:
```
https://student-performance-app-2026.herokuapp.com
```

## إعدادات إضافية:
```bash
# إضافة متغيرات البيئة
heroku config:set FLASK_ENV=production
heroku config:set SECRET_KEY=your-secret-key-here

# مراقبة السجلات
heroku logs --tail
```