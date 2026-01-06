# -*- coding: utf-8 -*-
import pandas as pd
import numpy as np
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_squared_error, r2_score, mean_absolute_error
from sklearn.preprocessing import LabelEncoder

def generate_detailed_report():
    print("="*60)
    print("تقرير شامل عن تطبيق خوارزمية Linear Regression")
    print("على بيانات أداء الطلاب")
    print("="*60)
    
    # قراءة البيانات
    df = pd.read_csv('extracted1/StudentPerformance.csv')
    
    # معالجة البيانات
    le = LabelEncoder()
    df['Extracurricular_Activities_Encoded'] = le.fit_transform(df['Extracurricular Activities'])
    
    features = ['Hours Studied', 'Previous Scores', 'Extracurricular_Activities_Encoded', 
               'Sleep Hours', 'Sample Question Papers Practiced']
    target = 'Performance Index'
    
    X = df[features]
    y = df[target]
    
    # تقسيم البيانات
    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)
    
    # تدريب النموذج
    model = LinearRegression()
    model.fit(X_train, y_train)
    
    # التنبؤ
    y_pred_train = model.predict(X_train)
    y_pred_test = model.predict(X_test)
    
    # حساب المقاييس
    train_r2 = r2_score(y_train, y_pred_train)
    test_r2 = r2_score(y_test, y_pred_test)
    train_rmse = np.sqrt(mean_squared_error(y_train, y_pred_train))
    test_rmse = np.sqrt(mean_squared_error(y_test, y_pred_test))
    
    print("\n1. معلومات عامة عن البيانات:")
    print(f"   - عدد العينات: {df.shape[0]:,}")
    print(f"   - عدد المتغيرات المستقلة: {len(features)}")
    print(f"   - المتغير التابع: {target}")
    print(f"   - نطاق المتغير التابع: {y.min():.1f} - {y.max():.1f}")
    
    print("\n2. تقسيم البيانات:")
    print(f"   - بيانات التدريب: {X_train.shape[0]:,} عينة ({X_train.shape[0]/df.shape[0]*100:.1f}%)")
    print(f"   - بيانات الاختبار: {X_test.shape[0]:,} عينة ({X_test.shape[0]/df.shape[0]*100:.1f}%)")
    
    print("\n3. نتائج النموذج:")
    print(f"   - دقة التدريب (R²): {train_r2:.4f} ({train_r2*100:.2f}%)")
    print(f"   - دقة الاختبار (R²): {test_r2:.4f} ({test_r2*100:.2f}%)")
    print(f"   - خطأ التدريب (RMSE): {train_rmse:.4f}")
    print(f"   - خطأ الاختبار (RMSE): {test_rmse:.4f}")
    
    print("\n4. معادلة النموذج:")
    print(f"   Performance Index = {model.intercept_:.4f}")
    for feature, coef in zip(features, model.coef_):
        sign = "+" if coef >= 0 else ""
        print(f"                     {sign} {coef:.4f} × {feature}")
    
    print("\n5. تفسير المعاملات:")
    feature_names = [
        "ساعات الدراسة",
        "الدرجات السابقة", 
        "الأنشطة اللامنهجية",
        "ساعات النوم",
        "أوراق الأسئلة المُمارسة"
    ]
    
    for i, (feature, coef, ar_name) in enumerate(zip(features, model.coef_, feature_names)):
        print(f"   - {ar_name}: {coef:.4f}")
        if coef > 0:
            print(f"     كل وحدة زيادة تحسن الأداء بـ {coef:.4f} نقطة")
        else:
            print(f"     كل وحدة زيادة تقلل الأداء بـ {abs(coef):.4f} نقطة")
    
    # ترتيب المتغيرات حسب الأهمية
    importance_df = pd.DataFrame({
        'Variable': feature_names,
        'Coefficient': model.coef_,
        'Abs_Coefficient': np.abs(model.coef_)
    }).sort_values('Abs_Coefficient', ascending=False)
    
    print("\n6. ترتيب المتغيرات حسب الأهمية:")
    for i, row in importance_df.iterrows():
        print(f"   {row.name + 1}. {row['Variable']}: {row['Coefficient']:.4f}")
    
    print("\n7. تقييم جودة النموذج:")
    if test_r2 > 0.9:
        print("   ✓ النموذج ممتاز (R² > 0.9)")
    elif test_r2 > 0.8:
        print("   ✓ النموذج جيد جداً (R² > 0.8)")
    elif test_r2 > 0.7:
        print("   ✓ النموذج جيد (R² > 0.7)")
    else:
        print("   ⚠ النموذج يحتاج تحسين (R² < 0.7)")
    
    # فحص الإفراط في التدريب
    overfitting = abs(train_r2 - test_r2)
    if overfitting < 0.05:
        print("   ✓ لا يوجد إفراط في التدريب")
    elif overfitting < 0.1:
        print("   ⚠ إفراط طفيف في التدريب")
    else:
        print("   ❌ إفراط واضح في التدريب")
    
    print("\n8. أمثلة على التنبؤ:")
    # مثال 1: طالب متفوق
    sample1 = np.array([[9, 95, 1, 8, 9]])
    pred1 = model.predict(sample1)[0]
    print(f"   - طالب متفوق (9 ساعات دراسة، 95 درجة سابقة، أنشطة، 8 ساعات نوم، 9 أوراق): {pred1:.1f}")
    
    # مثال 2: طالب متوسط
    sample2 = np.array([[5, 70, 0, 6, 3]])
    pred2 = model.predict(sample2)[0]
    print(f"   - طالب متوسط (5 ساعات دراسة، 70 درجة سابقة، لا أنشطة، 6 ساعات نوم، 3 أوراق): {pred2:.1f}")
    
    # مثال 3: طالب ضعيف
    sample3 = np.array([[2, 40, 0, 4, 1]])
    pred3 = model.predict(sample3)[0]
    print(f"   - طالب ضعيف (2 ساعة دراسة، 40 درجة سابقة، لا أنشطة، 4 ساعات نوم، ورقة واحدة): {pred3:.1f}")
    
    print("\n9. التوصيات:")
    print("   - الدرجات السابقة هي أقوى مؤشر على الأداء المستقبلي")
    print("   - زيادة ساعات الدراسة تحسن الأداء بشكل كبير")
    print("   - المشاركة في الأنشطة اللامنهجية لها تأثير إيجابي")
    print("   - النوم الكافي مهم لتحسين الأداء")
    print("   - ممارسة أوراق الأسئلة النموذجية مفيدة ولكن تأثيرها محدود")
    
    print("\n" + "="*60)
    print("انتهى التقرير")
    print("="*60)

if __name__ == "__main__":
    generate_detailed_report()