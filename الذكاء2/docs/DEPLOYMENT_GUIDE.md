# دليل النشر - نظام توقع أداء الطلاب

## نظرة عامة
هذا الدليل يوضح كيفية نشر تطبيق توقع أداء الطلاب على منصات مختلفة.

## المتطلبات الأساسية
- Python 3.11+
- Git
- حساب على منصة النشر المختارة

## الملفات المطلوبة للنشر
```
├── flask_app.py              # التطبيق الرئيسي
├── requirements.txt          # المكتبات المطلوبة
├── Procfile                 # إعدادات التشغيل
├── runtime.txt              # إصدار Python
├── app.json                 # معلومات التطبيق
├── templates/               # قوالب HTML
├── extracted1/              # مجلد البيانات
│   └── StudentPerformance.csv
└── users.db                 # قاعدة البيانات (ستُنشأ تلقائياً)
```

## 1. النشر على Heroku

### الخطوات:
1. **إنشاء حساب Heroku**
   ```bash
   # تثبيت Heroku CLI
   # Windows: تحميل من https://devcenter.heroku.com/articles/heroku-cli
   # macOS: brew install heroku/brew/heroku
   # Ubuntu: sudo snap install --classic heroku
   ```

2. **تسجيل الدخول**
   ```bash
   heroku login
   ```

3. **إنشاء تطبيق جديد**
   ```bash
   heroku create student-performance-app
   ```

4. **رفع الكود**
   ```bash
   git init
   git add .
   git commit -m "Initial commit"
   git push heroku main
   ```

5. **فتح التطبيق**
   ```bash
   heroku open
   ```

### الرابط المباشر للنشر:
[![Deploy to Heroku](https://www.herokucdn.com/deploy/button.svg)](https://heroku.com/deploy)

## 2. النشر على Railway

### الخطوات:
1. **إنشاء حساب على Railway.app**
2. **ربط مستودع GitHub**
3. **اختيار المشروع**
4. **النشر التلقائي**

### الرابط المباشر:
[![Deploy on Railway](https://railway.app/button.svg)](https://railway.app/new/template)

## 3. النشر على Render

### الخطوات:
1. **إنشاء حساب على Render.com**
2. **إنشاء Web Service جديد**
3. **ربط مستودع GitHub**
4. **إعدادات البناء:**
   - Build Command: `pip install -r requirements.txt`
   - Start Command: `gunicorn flask_app:app`

## 4. النشر على PythonAnywhere

### الخطوات:
1. **إنشاء حساب على PythonAnywhere**
2. **رفع الملفات عبر Files**
3. **إنشاء Web App جديد**
4. **تكوين WSGI file:**
   ```python
   import sys
   import os
   
   path = '/home/yourusername/student-performance-app'
   if path not in sys.path:
       sys.path.append(path)
   
   from flask_app import app as application
   ```

## 5. النشر على DigitalOcean App Platform

### الخطوات:
1. **إنشاء حساب DigitalOcean**
2. **إنشاء App جديد**
3. **ربط مستودع GitHub**
4. **إعدادات التطبيق:**
   - Type: Web Service
   - Source: GitHub Repository
   - Build Command: `pip install -r requirements.txt`
   - Run Command: `gunicorn flask_app:app`

## 6. النشر المحلي للتطوير

### تشغيل محلي:
```bash
# تثبيت المتطلبات
pip install -r requirements.txt

# تشغيل التطبيق
python flask_app.py
```

### الوصول:
- الرابط: http://localhost:5000
- المنفذ: 5000

## متغيرات البيئة المطلوبة

### للإنتاج:
```bash
FLASK_ENV=production
SECRET_KEY=your-secret-key-here
```

### للتطوير:
```bash
FLASK_ENV=development
FLASK_DEBUG=1
```

## إعدادات قاعدة البيانات

### SQLite (افتراضي):
- الملف: `users.db`
- يُنشأ تلقائياً عند أول تشغيل
- مناسب للتطبيقات الصغيرة

### PostgreSQL (للإنتاج):
```python
# في flask_app.py
import os
DATABASE_URL = os.environ.get('DATABASE_URL', 'sqlite:///users.db')
```

## اختبار التطبيق بعد النشر

### الصفحات المطلوب اختبارها:
1. **الصفحة الرئيسية** - `/`
2. **تسجيل الدخول** - `/login`
3. **إنشاء حساب** - `/register`
4. **لوحة التحكم** - `/dashboard`
5. **سجل التنبؤات** - `/history`
6. **API معلومات النموذج** - `/api/model-info`

### اختبار الوظائف:
- ✅ إنشاء حساب جديد
- ✅ تسجيل الدخول
- ✅ إدخال البيانات
- ✅ توقع الأداء
- ✅ عرض النتائج
- ✅ حفظ التنبؤات
- ✅ عرض السجل

## استكشاف الأخطاء

### مشاكل شائعة:

1. **خطأ في تحميل البيانات:**
   ```
   FileNotFoundError: StudentPerformance.csv
   ```
   **الحل:** تأكد من وجود الملف في مجلد `extracted1/`

2. **خطأ في قاعدة البيانات:**
   ```
   sqlite3.OperationalError: no such table
   ```
   **الحل:** سيتم إنشاء الجداول تلقائياً عند أول تشغيل

3. **خطأ في المكتبات:**
   ```
   ModuleNotFoundError: No module named 'sklearn'
   ```
   **الحل:** تأكد من تثبيت جميع المتطلبات من `requirements.txt`

### سجلات الأخطاء:
```bash
# Heroku
heroku logs --tail

# Railway
railway logs

# محلي
python flask_app.py
```

## الأمان والحماية

### إعدادات الأمان:
1. **تشفير كلمات المرور** - SHA-256
2. **حماية الجلسات** - Flask Sessions
3. **التحقق من المدخلات** - Form Validation
4. **حماية من SQL Injection** - Parameterized Queries

### نصائح الأمان:
- استخدم HTTPS في الإنتاج
- غيّر SECRET_KEY بانتظام
- فعّل CSRF Protection
- استخدم قاعدة بيانات منفصلة للإنتاج

## المراقبة والصيانة

### مراقبة الأداء:
- استخدام أدوات مراقبة المنصة
- تتبع استخدام الذاكرة والمعالج
- مراقبة أوقات الاستجابة

### النسخ الاحتياطي:
- نسخ احتياطي دوري لقاعدة البيانات
- حفظ ملفات التكوين
- توثيق التغييرات

## الدعم والمساعدة

### الموارد المفيدة:
- [وثائق Flask](https://flask.palletsprojects.com/)
- [وثائق Heroku](https://devcenter.heroku.com/)
- [وثائق scikit-learn](https://scikit-learn.org/)

### التواصل:
- GitHub Issues للمشاكل التقنية
- البريد الإلكتروني للاستفسارات العامة

---

**ملاحظة:** تأكد من اختبار التطبيق محلياً قبل النشر على أي منصة.