from flask import Flask
import os

app = Flask(__name__)

@app.route('/')
def hello():
    return '''
    <html>
    <head><meta charset="UTF-8"><title>نظام توقع أداء الطلاب</title></head>
    <body style="font-family: Arial; text-align: center; padding: 50px; background: #f0f8ff;">
        <h1>🎓 نظام توقع أداء الطلاب</h1>
        <h2>✅ التطبيق يعمل بنجاح!</h2>
        <p>تم نشر التطبيق على Render بنجاح</p>
        <p>🌐 الرابط: alamri770.onrender.com</p>
    </body>
    </html>
    '''

if __name__ == '__main__':
    port = int(os.environ.get('PORT', 5000))
    app.run(host='0.0.0.0', port=port)
