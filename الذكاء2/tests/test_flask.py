#!/usr/bin/env python3
# -*- coding: utf-8 -*-

"""
اختبار سريع لتطبيق Flask
"""

import os
import sys

def test_imports():
    """اختبار استيراد المكتبات المطلوبة"""
    try:
        import flask
        print("✅ Flask متاح")
        
        import pandas
        print("✅ Pandas متاح")
        
        import numpy
        print("✅ NumPy متاح")
        
        import sklearn
        print("✅ scikit-learn متاح")
        
        return True
    except ImportError as e:
        print(f"❌ خطأ في الاستيراد: {e}")
        return False

def test_dataset():
    """اختبار وجود ملف البيانات"""
    paths = [
        'extracted1/StudentPerformance.csv',
        'StudentPerformance.csv',
        'الذكاء2/extracted1/StudentPerformance.csv'
    ]
    
    for path in paths:
        if os.path.exists(path):
            print(f"✅ تم العثور على البيانات: {path}")
            return True
    
    print("❌ لم يتم العثور على ملف البيانات")
    return False

def test_templates():
    """اختبار وجود ملفات القوالب"""
    templates = [
        'templates/base.html',
        'templates/index.html',
        'templates/login.html',
        'templates/register.html',
        'templates/dashboard.html',
        'templates/history.html'
    ]
    
    missing = []
    for template in templates:
        if os.path.exists(template):
            print(f"✅ {template}")
        else:
            print(f"❌ {template}")
            missing.append(template)
    
    return len(missing) == 0

def main():
    """تشغيل جميع الاختبارات"""
    print("🧪 اختبار تطبيق Flask...")
    print("=" * 50)
    
    # اختبار المكتبات
    print("\n📦 اختبار المكتبات:")
    imports_ok = test_imports()
    
    # اختبار البيانات
    print("\n📊 اختبار البيانات:")
    dataset_ok = test_dataset()
    
    # اختبار القوالب
    print("\n📄 اختبار القوالب:")
    templates_ok = test_templates()
    
    # النتيجة النهائية
    print("\n" + "=" * 50)
    if imports_ok and dataset_ok and templates_ok:
        print("🎉 جميع الاختبارات نجحت! التطبيق جاهز للتشغيل")
        print("\n🚀 لتشغيل التطبيق:")
        print("   python flask_app.py")
        print("   ثم انتقل إلى: http://localhost:5000")
    else:
        print("⚠️  بعض الاختبارات فشلت. يرجى إصلاح المشاكل أولاً")
        
        if not imports_ok:
            print("   - قم بتثبيت المكتبات: pip install -r requirements.txt")
        
        if not dataset_ok:
            print("   - تأكد من وجود ملف StudentPerformance.csv")
        
        if not templates_ok:
            print("   - تأكد من وجود جميع ملفات القوالب")

if __name__ == "__main__":
    main()