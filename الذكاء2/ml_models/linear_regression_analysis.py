# -*- coding: utf-8 -*-
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_squared_error, r2_score, mean_absolute_error
from sklearn.preprocessing import LabelEncoder
import warnings
warnings.filterwarnings('ignore')

# Set matplotlib to use a font that supports Arabic (optional)
plt.rcParams['font.size'] = 10

def main():
    print("Reading student performance data...")
    df = pd.read_csv('extracted1/StudentPerformance.csv')
    
    print("Basic information about the data:")
    print(f"Number of rows: {df.shape[0]}")
    print(f"Number of columns: {df.shape[1]}")
    print("\nFirst 5 rows:")
    print(df.head())
    
    print("\nColumn information:")
    print(df.info())
    
    print("\nDescriptive statistics:")
    print(df.describe())
    
    # Data preprocessing
    print("\n" + "="*50)
    print("Data preprocessing...")
    
    # Convert text column to numeric
    le = LabelEncoder()
    df['Extracurricular_Activities_Encoded'] = le.fit_transform(df['Extracurricular Activities'])
    
    # Check for missing values
    print(f"Missing values in each column:")
    print(df.isnull().sum())
    
    # Remove missing values if any
    df_clean = df.dropna()
    print(f"Number of rows after removing missing values: {df_clean.shape[0]}")
    
    # Define independent and dependent variables
    features = ['Hours Studied', 'Previous Scores', 'Extracurricular_Activities_Encoded', 
               'Sleep Hours', 'Sample Question Papers Practiced']
    target = 'Performance Index'
    
    X = df_clean[features]
    y = df_clean[target]
    
    print(f"\nIndependent variables: {features}")
    print(f"Dependent variable: {target}")
    
    # Split the data
    print("\n" + "="*50)
    print("Splitting data into training and testing...")
    
    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)
    
    print(f"Training data size: {X_train.shape[0]} samples")
    print(f"Testing data size: {X_test.shape[0]} samples")
    
    # Apply Linear Regression
    print("\n" + "="*50)
    print("Applying Linear Regression algorithm...")
    
    model = LinearRegression()
    model.fit(X_train, y_train)
    
    # Make predictions
    y_pred_train = model.predict(X_train)
    y_pred_test = model.predict(X_test)
    
    print("Model trained successfully!")
    
    # Evaluate the model
    print("\n" + "="*50)
    print("Model performance evaluation...")
    
    # Calculate metrics for training data
    train_mse = mean_squared_error(y_train, y_pred_train)
    train_rmse = np.sqrt(train_mse)
    train_mae = mean_absolute_error(y_train, y_pred_train)
    train_r2 = r2_score(y_train, y_pred_train)
    
    # Calculate metrics for test data
    test_mse = mean_squared_error(y_test, y_pred_test)
    test_rmse = np.sqrt(test_mse)
    test_mae = mean_absolute_error(y_test, y_pred_test)
    test_r2 = r2_score(y_test, y_pred_test)
    
    print("Training Results:")
    print(f"  Mean Squared Error (MSE): {train_mse:.4f}")
    print(f"  Root Mean Squared Error (RMSE): {train_rmse:.4f}")
    print(f"  Mean Absolute Error (MAE): {train_mae:.4f}")
    print(f"  R² Score: {train_r2:.4f}")
    
    print("\nTesting Results:")
    print(f"  Mean Squared Error (MSE): {test_mse:.4f}")
    print(f"  Root Mean Squared Error (RMSE): {test_rmse:.4f}")
    print(f"  Mean Absolute Error (MAE): {test_mae:.4f}")
    print(f"  R² Score: {test_r2:.4f}")
    
    # Model coefficients
    print("\n" + "="*50)
    print("Model coefficients:")
    print(f"Intercept: {model.intercept_:.4f}")
    print("Coefficients:")
    for feature, coef in zip(features, model.coef_):
        print(f"  {feature}: {coef:.4f}")
    
    # Create visualizations
    print("\n" + "="*50)
    print("Creating visualizations...")
    
    plt.style.use('default')
    fig, axes = plt.subplots(2, 2, figsize=(15, 12))
    
    # 1. Actual vs Predicted values
    axes[0, 0].scatter(y_test, y_pred_test, alpha=0.6, color='blue')
    axes[0, 0].plot([y_test.min(), y_test.max()], [y_test.min(), y_test.max()], 'r--', lw=2)
    axes[0, 0].set_xlabel('Actual Values')
    axes[0, 0].set_ylabel('Predicted Values')
    axes[0, 0].set_title('Actual vs Predicted Values')
    axes[0, 0].grid(True, alpha=0.3)
    
    # 2. Residuals distribution
    residuals = y_test - y_pred_test
    axes[0, 1].scatter(y_pred_test, residuals, alpha=0.6, color='green')
    axes[0, 1].axhline(y=0, color='r', linestyle='--')
    axes[0, 1].set_xlabel('Predicted Values')
    axes[0, 1].set_ylabel('Residuals')
    axes[0, 1].set_title('Residuals Distribution')
    axes[0, 1].grid(True, alpha=0.3)
    
    # 3. Histogram of residuals
    axes[1, 0].hist(residuals, bins=30, alpha=0.7, color='orange', edgecolor='black')
    axes[1, 0].set_xlabel('Residuals')
    axes[1, 0].set_ylabel('Frequency')
    axes[1, 0].set_title('Residuals Histogram')
    axes[1, 0].grid(True, alpha=0.3)
    
    # 4. Feature importance
    feature_importance = abs(model.coef_)
    axes[1, 1].barh(range(len(features)), feature_importance, color='purple', alpha=0.7)
    axes[1, 1].set_yticks(range(len(features)))
    axes[1, 1].set_yticklabels([f.replace('_', ' ') for f in features])
    axes[1, 1].set_xlabel('Feature Importance (Absolute Coefficient Value)')
    axes[1, 1].set_title('Feature Importance')
    axes[1, 1].grid(True, alpha=0.3)
    
    plt.tight_layout()
    plt.savefig('linear_regression_results.png', dpi=300, bbox_inches='tight')
    plt.show()
    
    print("Visualizations saved to: linear_regression_results.png")
    
    # Correlation matrix
    print("\n" + "="*50)
    print("Correlation matrix...")
    
    plt.figure(figsize=(10, 8))
    correlation_matrix = df_clean[features + [target]].corr()
    sns.heatmap(correlation_matrix, annot=True, cmap='coolwarm', center=0, 
                square=True, fmt='.3f', cbar_kws={'shrink': 0.8})
    plt.title('Correlation Matrix Between Variables')
    plt.tight_layout()
    plt.savefig('correlation_matrix.png', dpi=300, bbox_inches='tight')
    plt.show()
    
    print("Correlation matrix saved to: correlation_matrix.png")
    
    # Prediction on new sample
    print("\n" + "="*50)
    print("Example prediction on new sample...")
    
    # Create new sample for prediction
    new_sample = np.array([[7, 85, 1, 8, 5]])  # hours=7, prev_scores=85, activities=yes, sleep=8, papers=5
    predicted_performance = model.predict(new_sample)
    
    print("New sample:")
    print(f"  Study Hours: {new_sample[0][0]}")
    print(f"  Previous Scores: {new_sample[0][1]}")
    print(f"  Extracurricular Activities: {'Yes' if new_sample[0][2] == 1 else 'No'}")
    print(f"  Sleep Hours: {new_sample[0][3]}")
    print(f"  Sample Papers Practiced: {new_sample[0][4]}")
    print(f"Predicted Performance Index: {predicted_performance[0]:.2f}")
    
    print("\n" + "="*50)
    print("Linear Regression analysis completed successfully!")
    print("="*50)

if __name__ == "__main__":
    main()