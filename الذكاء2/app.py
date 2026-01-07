from flask import Flask
import os

app = Flask(__name__)

@app.route('/')
def home():
    return '''
    <!DOCTYPE html>
    <html lang="ar" dir="rtl">
    <head>
        <meta charset="UTF-8">
        <title>نظام توقع أداء الطلاب</title>
        <style>
            body { 
                font-family: Arial, sans-serif; 
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                margin: 0; 
                padding: 20px; 
                min-height: 100vh;
                display: flex;
                align-items: center;
                justify-content: center;
            }
            .container { 
                max-width: 600px; 
                background: white; 
                padding: 40px; 
                border-radius: 15px; 
                text-align: center;
                box-shadow: 0 10px 30px rgba(0,0,0,0.2);
            }
            h1 { color: #333; margin-bottom: 20px; }
            .btn { 
                display: inline-block; 
                padding: 15px 30px; 
                margin: 10px; 
                background: #007bff; 
                color: white; 
                text-decoration: none; 
                border-radius: 25px;
                transition: all 0.3s;
            }
            .btn:hover { 
                background: #0056b3; 
                transform: translateY(-2px);
            }
            .success {
                background: #d4edda;
                color: #155724;
                padding: 20px;
                border-radius: 10px;
                margin: 20px 0;
            }
        </style>
    </head>
    <body>
        <div class="container">
            <h1>🎓 نظام توقع أداء الطلاب</h1>
            
            <div class="success">
                <h2>✅ تم النشر بنجاح!</h2>
                <p>التطبيق يعمل الآن على Render</p>
            </div>
            
            <p>نظام ذكي لتوقع أداء الطلاب باستخدام الذكاء الاصطناعي</p>
            
            <a href="/test" class="btn">🧪 اختبار النظام</a>
            <a href="/about" class="btn">ℹ️ حول المشروع</a>
        </div>
    </body>
    </html>
    '''

@app.route('/test')
def test():
    return '''
    <html lang="ar" dir="rtl">
    <head><meta charset="UTF-8"><title>اختبار النظام</title></head>
    <body style="font-family: Arial; text-align: center; padding: 50px; background: #f0f8ff;">
        <div style="max-width: 500px; margin: 0 auto; background: white; padding: 40px; border-radius: 10px;">
            <h1>🎉 النظام يعمل بنجاح!</h1>
            <p>✅ تم نشر التطبيق على Render بنجاح</p>
            <p>🚀 الخادم يستجيب بشكل طبيعي</p>
            <p>🌐 الرابط: <strong>alamri770.onrender.com</strong></p>
            <a href="/" style="display: inline-block; margin-top: 20px; padding: 10px 20px; background: #28a745; color: white; text-decoration: none; border-radius: 5px;">العودة للرئيسية</a>
        </div>
    </body>
    </html>
    '''

@app.route('/about')
def about():
    return '''
    <html lang="ar" dir="rtl">
    <head><meta charset="UTF-8"><title>حول المشروع</title></head>
    <body style="font-family: Arial; padding: 50px; background: #f5f5f5;">
        <div style="max-width: 600px; margin: 0 auto; background: white; padding: 40px; border-radius: 10px;">
            <h1>📋 حول المشروع</h1>
            <h3>🎯 الهدف:</h3>
            <p>نظام ذكي لتوقع أداء الطلاب باستخدام تعلم الآلة</p>
            
            <h3>🛠️ التقنيات المستخدمة:</h3>
            <ul style="text-align: right;">
                <li>Python Flask - إطار العمل</li>
                <li>Scikit-learn - تعلم الآلة</li>
                <li>Render.com - منصة النشر</li>
                <li>GitHub - إدارة الكود</li>
            </ul>
            
            <h3>✨ المميزات:</h3>
            <ul style="text-align: right;">
                <li>واجهة عربية سهلة الاستخدام</li>
                <li>نشر مجاني على الإنترنت</li>
                <li>استجابة سريعة</li>
                <li>تصميم متجاوب</li>
            </ul>
            
            <a href="/" style="display: inline-block; margin-top: 20px; padding: 10px 20px; background: #007bff; color: white; text-decoration: none; border-radius: 5px;">العودة للرئيسية</a>
        </div>
    </body>
    </html>
    '''

if __name__ == '__main__':
    port = int(os.environ.get('PORT', 5000))
    print(f"🚀 تشغيل نظام توقع أداء الطلاب على المنفذ {port}")
    print("✅ الخادم جاهز للاستقبال")
    app.run(host='0.0.0.0', port=port, debug=False)
