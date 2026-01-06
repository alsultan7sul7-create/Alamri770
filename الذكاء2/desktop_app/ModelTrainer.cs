using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace StudentPerformanceApp
{
    public class ModelTrainer
    {
        public class TrainingData
        {
            public int HoursStudied { get; set; }
            public int PreviousScores { get; set; }
            public bool ExtracurricularActivities { get; set; }
            public int SleepHours { get; set; }
            public int SamplePapers { get; set; }
            public double PerformanceIndex { get; set; }
        }

        public class ModelCoefficients
        {
            public double Intercept { get; set; }
            public double[] Coefficients { get; set; }
            public double R2Score { get; set; }
            public double RMSE { get; set; }
            public int TrainingDataCount { get; set; }
        }

        public static ModelCoefficients TrainModelFromDataset(string csvFilePath)
        {
            try
            {
                // قراءة البيانات من CSV
                var trainingData = LoadDataFromCSV(csvFilePath);
                
                if (trainingData.Count == 0)
                {
                    throw new Exception("لا توجد بيانات في الملف");
                }

                // تدريب النموذج باستخدام Linear Regression
                var coefficients = PerformLinearRegression(trainingData);
                
                return coefficients;
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تدريب النموذج: {ex.Message}");
            }
        }

        private static List<TrainingData> LoadDataFromCSV(string filePath)
        {
            var data = new List<TrainingData>();
            
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"الملف غير موجود: {filePath}");
            }

            var lines = File.ReadAllLines(filePath);
            
            // تخطي السطر الأول (العناوين)
            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    var parts = lines[i].Split(',');
                    
                    if (parts.Length >= 6)
                    {
                        var record = new TrainingData
                        {
                            HoursStudied = int.Parse(parts[0].Trim()),
                            PreviousScores = int.Parse(parts[1].Trim()),
                            ExtracurricularActivities = parts[2].Trim().ToLower() == "yes",
                            SleepHours = int.Parse(parts[3].Trim()),
                            SamplePapers = int.Parse(parts[4].Trim()),
                            PerformanceIndex = double.Parse(parts[5].Trim(), CultureInfo.InvariantCulture)
                        };
                        
                        data.Add(record);
                    }
                }
                catch (Exception ex)
                {
                    // تجاهل الأسطر التي بها أخطاء وإكمال القراءة
                    Console.WriteLine($"خطأ في قراءة السطر {i + 1}: {ex.Message}");
                }
            }

            return data;
        }

        private static ModelCoefficients PerformLinearRegression(List<TrainingData> data)
        {
            int n = data.Count;
            int numFeatures = 5; // عدد المتغيرات المستقلة

            // إنشاء مصفوفة X (المتغيرات المستقلة) ومتجه y (المتغير التابع)
            double[,] X = new double[n, numFeatures + 1]; // +1 للـ intercept
            double[] y = new double[n];

            // ملء البيانات
            for (int i = 0; i < n; i++)
            {
                X[i, 0] = 1; // intercept term
                X[i, 1] = data[i].HoursStudied;
                X[i, 2] = data[i].PreviousScores;
                X[i, 3] = data[i].ExtracurricularActivities ? 1 : 0;
                X[i, 4] = data[i].SleepHours;
                X[i, 5] = data[i].SamplePapers;
                
                y[i] = data[i].PerformanceIndex;
            }

            // حساب المعاملات باستخدام Normal Equation: β = (X^T * X)^(-1) * X^T * y
            var coefficients = SolveNormalEquation(X, y, n, numFeatures + 1);

            // حساب R² و RMSE
            double r2 = CalculateR2(X, y, coefficients, n, numFeatures + 1);
            double rmse = CalculateRMSE(X, y, coefficients, n, numFeatures + 1);

            return new ModelCoefficients
            {
                Intercept = coefficients[0],
                Coefficients = coefficients.Skip(1).ToArray(),
                R2Score = r2,
                RMSE = rmse,
                TrainingDataCount = n
            };
        }

        private static double[] SolveNormalEquation(double[,] X, double[] y, int n, int p)
        {
            // حساب X^T * X
            double[,] XTX = new double[p, p];
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < n; k++)
                    {
                        sum += X[k, i] * X[k, j];
                    }
                    XTX[i, j] = sum;
                }
            }

            // حساب X^T * y
            double[] XTy = new double[p];
            for (int i = 0; i < p; i++)
            {
                double sum = 0;
                for (int k = 0; k < n; k++)
                {
                    sum += X[k, i] * y[k];
                }
                XTy[i] = sum;
            }

            // حل النظام الخطي (X^T * X) * β = X^T * y
            return SolveLinearSystem(XTX, XTy, p);
        }

        private static double[] SolveLinearSystem(double[,] A, double[] b, int n)
        {
            // استخدام Gaussian Elimination مع Partial Pivoting
            double[,] augmented = new double[n, n + 1];
            
            // إنشاء المصفوفة المعززة
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmented[i, j] = A[i, j];
                }
                augmented[i, n] = b[i];
            }

            // Forward elimination
            for (int i = 0; i < n; i++)
            {
                // البحث عن أكبر عنصر للـ pivoting
                int maxRow = i;
                for (int k = i + 1; k < n; k++)
                {
                    if (Math.Abs(augmented[k, i]) > Math.Abs(augmented[maxRow, i]))
                    {
                        maxRow = k;
                    }
                }

                // تبديل الصفوف
                for (int k = i; k < n + 1; k++)
                {
                    double temp = augmented[maxRow, k];
                    augmented[maxRow, k] = augmented[i, k];
                    augmented[i, k] = temp;
                }

                // جعل جميع الصفوف تحت الصف الحالي صفر في العمود i
                for (int k = i + 1; k < n; k++)
                {
                    double factor = augmented[k, i] / augmented[i, i];
                    for (int j = i; j < n + 1; j++)
                    {
                        augmented[k, j] -= factor * augmented[i, j];
                    }
                }
            }

            // Back substitution
            double[] solution = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                solution[i] = augmented[i, n];
                for (int j = i + 1; j < n; j++)
                {
                    solution[i] -= augmented[i, j] * solution[j];
                }
                solution[i] /= augmented[i, i];
            }

            return solution;
        }

        private static double CalculateR2(double[,] X, double[] y, double[] coefficients, int n, int p)
        {
            // حساب المتوسط
            double yMean = y.Average();

            // حساب TSS و RSS
            double tss = 0, rss = 0;
            
            for (int i = 0; i < n; i++)
            {
                // التنبؤ
                double prediction = 0;
                for (int j = 0; j < p; j++)
                {
                    prediction += X[i, j] * coefficients[j];
                }

                tss += Math.Pow(y[i] - yMean, 2);
                rss += Math.Pow(y[i] - prediction, 2);
            }

            return 1 - (rss / tss);
        }

        private static double CalculateRMSE(double[,] X, double[] y, double[] coefficients, int n, int p)
        {
            double sumSquaredErrors = 0;

            for (int i = 0; i < n; i++)
            {
                // التنبؤ
                double prediction = 0;
                for (int j = 0; j < p; j++)
                {
                    prediction += X[i, j] * coefficients[j];
                }

                sumSquaredErrors += Math.Pow(y[i] - prediction, 2);
            }

            return Math.Sqrt(sumSquaredErrors / n);
        }

        // دالة لحفظ النموذج المدرب
        public static void SaveModelToFile(ModelCoefficients model, string filePath)
        {
            try
            {
                var lines = new List<string>
                {
                    $"Intercept:{model.Intercept}",
                    $"Coefficients:{string.Join(",", model.Coefficients)}",
                    $"R2Score:{model.R2Score}",
                    $"RMSE:{model.RMSE}",
                    $"TrainingDataCount:{model.TrainingDataCount}",
                    $"TrainingDate:{DateTime.Now:yyyy-MM-dd HH:mm:ss}"
                };

                File.WriteAllLines(filePath, lines);
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في حفظ النموذج: {ex.Message}");
            }
        }

        // دالة لتحميل النموذج من ملف
        public static ModelCoefficients LoadModelFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return null;
                }

                var lines = File.ReadAllLines(filePath);
                var model = new ModelCoefficients();

                foreach (var line in lines)
                {
                    var parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        switch (parts[0])
                        {
                            case "Intercept":
                                model.Intercept = double.Parse(parts[1], CultureInfo.InvariantCulture);
                                break;
                            case "Coefficients":
                                model.Coefficients = parts[1].Split(',')
                                    .Select(x => double.Parse(x, CultureInfo.InvariantCulture))
                                    .ToArray();
                                break;
                            case "R2Score":
                                model.R2Score = double.Parse(parts[1], CultureInfo.InvariantCulture);
                                break;
                            case "RMSE":
                                model.RMSE = double.Parse(parts[1], CultureInfo.InvariantCulture);
                                break;
                            case "TrainingDataCount":
                                model.TrainingDataCount = int.Parse(parts[1]);
                                break;
                        }
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception($"خطأ في تحميل النموذج: {ex.Message}");
            }
        }
    }
}