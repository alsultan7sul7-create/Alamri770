from flask import Flask
import os

app = Flask(__name__)

@app.route('/')
def home():
    return '''
    <html>
    <head>
        <meta charset="UTF-8">
        <title>نظام توقع أداء الطلاب</title>
        <style>
            body { font-family: Arial; text-align: center; padding: 50px; background: #f0f2f5; }
            .container { max-width: 600px; margin: 0 auto; background: white; padding: 40px; border-radius: 10px; }
            h1 { color: #333; }
            .btn { display: inline-block; padding: 15px 30px; margin: 10px; background: #007bff; color: white; text-decoration: none; border-radius: 5px; }
        </style>
    </head>
    <body>
        <div class="container">
            <h1>🎓 نظام توقع أداء الطلاب</h1>
            <p>نظام ذكي لتوقع أداء الطلاب باستخدام الذكاء الاصطناعي</p>
            <a href="/test" class="btn">اختبار النظام</a>
        </div>
    </body>
    </html>
    '''

@app.route('/test')
def test():
    return '''
    <html>
    <body style="font-family: Arial; text-align: center; padding: 50px;">
        <h1>✅ النظام يعمل بنجاح!</h1>
        <p>تم نشر التطبيق على Render بنجاح</p>
        <a href="/">العودة للرئيسية</a>
    </body>
    </html>
    '''

if __name__ == '__main__':
    port = int(os.environ.get('PORT', 5000))
    print(f"🚀 تشغيل التطبيق على المنفذ {port}")
    app.run(host='0.0.0.0', port=port, debug=False)
