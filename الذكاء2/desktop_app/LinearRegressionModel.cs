using System;
using System.IO;

namespace StudentPerformanceApp
{
    public static class LinearRegressionModel
    {
        private static ModelTrainer.ModelCoefficients _trainedModel;
        private static readonly object _lockObject = new object();

        // معاملات النموذج الافتراضية (في حالة عدم وجود dataset)
        private static readonly double DefaultIntercept = -33.9219;
        private static readonly double[] DefaultCoefficients = { 2.8525, 1.0170, 0.6086, 0.4769, 0.1918 };

        static LinearRegressionModel()
        {
            InitializeModel();
        }

        private static void InitializeModel()
        {
            lock (_lockObject)
            {
                try
                {
                    // محاولة تحميل النموذج المحفوظ
                    _trainedModel = ModelTrainer.LoadModelFromFile("trained_model.txt");

                    if (_trainedModel == null)
                    {
                        // محاولة تدريب النموذج من dataset
                        TrainModelFromDataset();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"تحذير: لا يمكن تحميل أو تدريب النموذج: {ex.Message}");
                    Console.WriteLine("سيتم استخدام النموذج الافتراضي");
                    _trainedModel = null;
                }
            }
        }

        private static void TrainModelFromDataset()
        {
            string[] possiblePaths = {
                "extracted1/StudentPerformance.csv",
                "StudentPerformance.csv",
                "../extracted1/StudentPerformance.csv",
                "../../الذكاء2/extracted1/StudentPerformance.csv"
            };

            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    try
                    {
                        Console.WriteLine($"تدريب النموذج من: {path}");
                        _trainedModel = ModelTrainer.TrainModelFromDataset(path);
                        
                        // حفظ النموذج المدرب
                        ModelTrainer.SaveModelToFile(_trainedModel, "trained_model.txt");
                        
                        Console.WriteLine($"تم تدريب النموذج بنجاح!");
                        Console.WriteLine($"دقة النموذج (R²): {_trainedModel.R2Score:F4}");
                        Console.WriteLine($"خطأ RMSE: {_trainedModel.RMSE:F4}");
                        Console.WriteLine($"عدد عينات التدريب: {_trainedModel.TrainingDataCount:N0}");
                        
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"خطأ في تدريب النموذج من {path}: {ex.Message}");
                    }
                }
            }
            
            Console.WriteLine("لم يتم العثور على dataset، سيتم استخدام النموذج الافتراضي");
        }

        public static void RetrainModel()
        {
            lock (_lockObject)
            {
                TrainModelFromDataset();
            }
        }

        public static ModelTrainer.ModelCoefficients GetModelInfo()
        {
            return _trainedModel;
        }

        public static double PredictPerformance(int hoursStudied, int previousScores, 
            bool hasExtracurricular, int sleepHours, int samplePapers)
        {
            // تحويل الأنشطة اللامنهجية إلى رقم
            int extracurricularEncoded = hasExtracurricular ? 1 : 0;

            double prediction;

            if (_trainedModel != null)
            {
                // استخدام النموذج المدرب من dataset
                prediction = _trainedModel.Intercept +
                    (_trainedModel.Coefficients[0] * hoursStudied) +
                    (_trainedModel.Coefficients[1] * previousScores) +
                    (_trainedModel.Coefficients[2] * extracurricularEncoded) +
                    (_trainedModel.Coefficients[3] * sleepHours) +
                    (_trainedModel.Coefficients[4] * samplePapers);
            }
            else
            {
                // استخدام النموذج الافتراضي
                prediction = DefaultIntercept +
                    (DefaultCoefficients[0] * hoursStudied) +
                    (DefaultCoefficients[1] * previousScores) +
                    (DefaultCoefficients[2] * extracurricularEncoded) +
                    (DefaultCoefficients[3] * sleepHours) +
                    (DefaultCoefficients[4] * samplePapers);
            }

            // تحديد النطاق بين 0 و 100
            return Math.Max(0, Math.Min(100, prediction));
        }

        public static string GetPerformanceLevel(double score)
        {
            if (score >= 90) return "ممتاز";
            if (score >= 80) return "جيد جداً";
            if (score >= 70) return "جيد";
            if (score >= 60) return "مقبول";
            return "ضعيف";
        }

        public static System.Drawing.Color GetPerformanceColor(double score)
        {
            if (score >= 90) return System.Drawing.Color.Green;
            if (score >= 80) return System.Drawing.Color.DodgerBlue;
            if (score >= 70) return System.Drawing.Color.Orange;
            if (score >= 60) return System.Drawing.Color.Gold;
            return System.Drawing.Color.Red;
        }

        public static string GetRecommendations(int hoursStudied, int previousScores, 
            bool hasExtracurricular, int sleepHours, int samplePapers)
        {
            var recommendations = new System.Text.StringBuilder();
            recommendations.AppendLine("التوصيات لتحسين الأداء:");
            recommendations.AppendLine();

            if (hoursStudied < 6)
            {
                recommendations.AppendLine("• زيادة ساعات الدراسة - كل ساعة إضافية تحسن الأداء بـ 2.85 نقطة");
            }

            if (previousScores < 70)
            {
                recommendations.AppendLine("• التركيز على تحسين الدرجات الحالية - أساس مهم للأداء المستقبلي");
            }

            if (!hasExtracurricular)
            {
                recommendations.AppendLine("• المشاركة في الأنشطة اللامنهجية - تحسن الأداء بـ 0.61 نقطة");
            }

            if (sleepHours < 7)
            {
                recommendations.AppendLine("• زيادة ساعات النوم - كل ساعة إضافية تحسن الأداء بـ 0.48 نقطة");
            }

            if (samplePapers < 5)
            {
                recommendations.AppendLine("• ممارسة المزيد من أوراق الأسئلة النموذجية");
            }

            if (recommendations.Length == 32) // فقط العنوان
            {
                recommendations.AppendLine("• أداؤك ممتاز! استمر على هذا المنوال");
            }

            return recommendations.ToString();
        }
    }
}