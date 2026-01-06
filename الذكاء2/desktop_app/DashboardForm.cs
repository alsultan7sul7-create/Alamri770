using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentPerformanceApp
{
    public partial class DashboardForm : Form
    {
        private User currentUser;
        private Panel headerPanel;
        private Panel inputPanel;
        private Panel resultPanel;
        private Label welcomeLabel;
        private Label titleLabel;
        private Button logoutButton;
        
        // Input controls
        private NumericUpDown hoursStudiedNumeric;
        private NumericUpDown previousScoresNumeric;
        private ComboBox extracurricularCombo;
        private NumericUpDown sleepHoursNumeric;
        private NumericUpDown samplePapersNumeric;
        private Button predictButton;
        
        // Result controls
        private Label resultLabel;
        private Label performanceLevelLabel;
        private RichTextBox recommendationsTextBox;
        private ProgressBar performanceProgressBar;

        public DashboardForm(User user)
        {
            currentUser = user;
            InitializeComponent();
            SetupUI();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 800);
            this.Text = "نظام توقع أداء الطلاب - لوحة التحكم";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(1200, 800);
            this.BackColor = Color.FromArgb(248, 249, 250);
            
            this.ResumeLayout(false);
        }

        private void SetupUI()
        {
            // Header Panel
            headerPanel = new Panel
            {
                Size = new Size(1200, 80),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(25, 25, 112),
                Dock = DockStyle.Top
            };

            // Welcome Label
            welcomeLabel = new Label
            {
                Text = $"مرحباً، {currentUser.FullName}",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(400, 30),
                Location = new Point(30, 15),
                RightToLeft = RightToLeft.Yes
            };

            // Title Label
            titleLabel = new Label
            {
                Text = "توقع الأداء الأكاديمي",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.FromArgb(200, 200, 200),
                Size = new Size(200, 25),
                Location = new Point(30, 45),
                RightToLeft = RightToLeft.Yes
            };

            // Logout Button
            logoutButton = new Button
            {
                Text = "تسجيل الخروج",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Size = new Size(120, 35),
                Location = new Point(1050, 22),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            logoutButton.FlatAppearance.BorderSize = 0;
            logoutButton.Click += LogoutButton_Click;

            headerPanel.Controls.AddRange(new Control[] { welcomeLabel, titleLabel, logoutButton });

            // Input Panel
            inputPanel = new Panel
            {
                Size = new Size(580, 650),
                Location = new Point(30, 100),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            inputPanel.Paint += Panel_Paint;

            SetupInputControls();

            // Result Panel
            resultPanel = new Panel
            {
                Size = new Size(550, 650),
                Location = new Point(620, 100),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            resultPanel.Paint += Panel_Paint;

            SetupResultControls();

            // Add panels to form
            this.Controls.AddRange(new Control[] { headerPanel, inputPanel, resultPanel });
        }

        private void SetupInputControls()
        {
            var titleLabel = new Label
            {
                Text = "إدخال بيانات الطالب",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 112),
                Size = new Size(250, 35),
                Location = new Point(30, 30),
                RightToLeft = RightToLeft.Yes
            };

            // Hours Studied
            var hoursLabel = new Label
            {
                Text = "ساعات الدراسة اليومية:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 58, 64),
                Size = new Size(180, 25),
                Location = new Point(30, 100),
                RightToLeft = RightToLeft.Yes
            };

            hoursStudiedNumeric = new NumericUpDown
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(520, 30),
                Location = new Point(30, 130),
                Minimum = 1,
                Maximum = 12,
                Value = 5
            };

            // Previous Scores
            var scoresLabel = new Label
            {
                Text = "الدرجات السابقة (من 100):",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 58, 64),
                Size = new Size(180, 25),
                Location = new Point(30, 180),
                RightToLeft = RightToLeft.Yes
            };

            previousScoresNumeric = new NumericUpDown
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(520, 30),
                Location = new Point(30, 210),
                Minimum = 0,
                Maximum = 100,
                Value = 70
            };

            // Extracurricular Activities
            var activitiesLabel = new Label
            {
                Text = "الأنشطة اللامنهجية:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 58, 64),
                Size = new Size(180, 25),
                Location = new Point(30, 260),
                RightToLeft = RightToLeft.Yes
            };

            extracurricularCombo = new ComboBox
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(520, 30),
                Location = new Point(30, 290),
                DropDownStyle = ComboBoxStyle.DropDownList,
                RightToLeft = RightToLeft.Yes
            };
            extracurricularCombo.Items.AddRange(new string[] { "نعم", "لا" });
            extracurricularCombo.SelectedIndex = 0;

            // Sleep Hours
            var sleepLabel = new Label
            {
                Text = "ساعات النوم اليومية:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 58, 64),
                Size = new Size(180, 25),
                Location = new Point(30, 340),
                RightToLeft = RightToLeft.Yes
            };

            sleepHoursNumeric = new NumericUpDown
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(520, 30),
                Location = new Point(30, 370),
                Minimum = 3,
                Maximum = 12,
                Value = 7
            };

            // Sample Papers
            var papersLabel = new Label
            {
                Text = "عدد أوراق الأسئلة المُمارسة:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 58, 64),
                Size = new Size(220, 25),
                Location = new Point(30, 420),
                RightToLeft = RightToLeft.Yes
            };

            samplePapersNumeric = new NumericUpDown
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(520, 30),
                Location = new Point(30, 450),
                Minimum = 0,
                Maximum = 20,
                Value = 3
            };

            // Predict Button
            predictButton = new Button
            {
                Text = "توقع الأداء",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Size = new Size(520, 50),
                Location = new Point(30, 520),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            predictButton.FlatAppearance.BorderSize = 0;
            predictButton.Click += PredictButton_Click;
            predictButton.MouseEnter += PredictButton_MouseEnter;
            predictButton.MouseLeave += PredictButton_MouseLeave;

            inputPanel.Controls.AddRange(new Control[] {
                titleLabel, hoursLabel, hoursStudiedNumeric, scoresLabel, previousScoresNumeric,
                activitiesLabel, extracurricularCombo, sleepLabel, sleepHoursNumeric,
                papersLabel, samplePapersNumeric, predictButton
            });
        }

        private void SetupResultControls()
        {
            var titleLabel = new Label
            {
                Text = "نتيجة التوقع",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 112),
                Size = new Size(150, 35),
                Location = new Point(30, 30),
                RightToLeft = RightToLeft.Yes
            };

            // Performance Progress Bar
            performanceProgressBar = new ProgressBar
            {
                Size = new Size(490, 30),
                Location = new Point(30, 100),
                Minimum = 0,
                Maximum = 100,
                Style = ProgressBarStyle.Continuous
            };

            // Result Label
            resultLabel = new Label
            {
                Text = "أدخل البيانات واضغط 'توقع الأداء'",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(108, 117, 125),
                Size = new Size(490, 40),
                Location = new Point(30, 150),
                TextAlign = ContentAlignment.MiddleCenter,
                RightToLeft = RightToLeft.Yes
            };

            // Performance Level Label
            performanceLevelLabel = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Size = new Size(490, 30),
                Location = new Point(30, 200),
                TextAlign = ContentAlignment.MiddleCenter,
                RightToLeft = RightToLeft.Yes
            };

            // Recommendations
            var recommendationsLabel = new Label
            {
                Text = "التوصيات:",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 112),
                Size = new Size(100, 25),
                Location = new Point(30, 250),
                RightToLeft = RightToLeft.Yes
            };

            recommendationsTextBox = new RichTextBox
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(490, 350),
                Location = new Point(30, 280),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(248, 249, 250),
                RightToLeft = RightToLeft.Yes
            };

            resultPanel.Controls.AddRange(new Control[] {
                titleLabel, performanceProgressBar, resultLabel, performanceLevelLabel,
                recommendationsLabel, recommendationsTextBox
            });
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            using (Pen pen = new Pen(Color.FromArgb(222, 226, 230), 2))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        private void PredictButton_MouseEnter(object sender, EventArgs e)
        {
            predictButton.BackColor = Color.FromArgb(33, 136, 56);
        }

        private void PredictButton_MouseLeave(object sender, EventArgs e)
        {
            predictButton.BackColor = Color.FromArgb(40, 167, 69);
        }

        private void PredictButton_Click(object sender, EventArgs e)
        {
            try
            {
                int hoursStudied = (int)hoursStudiedNumeric.Value;
                int previousScores = (int)previousScoresNumeric.Value;
                bool hasExtracurricular = extracurricularCombo.SelectedItem.ToString() == "نعم";
                int sleepHours = (int)sleepHoursNumeric.Value;
                int samplePapers = (int)samplePapersNumeric.Value;

                // التنبؤ باستخدام النموذج
                double prediction = LinearRegressionModel.PredictPerformance(
                    hoursStudied, previousScores, hasExtracurricular, sleepHours, samplePapers);

                // حفظ التنبؤ في قاعدة البيانات
                DatabaseManager.SavePrediction(currentUser.Id, hoursStudied, previousScores,
                    hasExtracurricular ? "نعم" : "لا", sleepHours, samplePapers, prediction);

                // عرض النتائج
                DisplayResults(prediction, hoursStudied, previousScores, hasExtracurricular, sleepHours, samplePapers);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء التنبؤ: {ex.Message}", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayResults(double prediction, int hoursStudied, int previousScores, 
            bool hasExtracurricular, int sleepHours, int samplePapers)
        {
            // تحديث شريط التقدم
            performanceProgressBar.Value = (int)Math.Round(prediction);

            // تحديث نص النتيجة
            resultLabel.Text = $"مؤشر الأداء المتوقع: {prediction:F1}";
            resultLabel.ForeColor = LinearRegressionModel.GetPerformanceColor(prediction);

            // تحديث مستوى الأداء
            string level = LinearRegressionModel.GetPerformanceLevel(prediction);
            performanceLevelLabel.Text = $"مستوى الأداء: {level}";
            performanceLevelLabel.ForeColor = LinearRegressionModel.GetPerformanceColor(prediction);

            // تحديث التوصيات
            string recommendations = LinearRegressionModel.GetRecommendations(
                hoursStudied, previousScores, hasExtracurricular, sleepHours, samplePapers);
            recommendationsTextBox.Text = recommendations;

            // إضافة معلومات إضافية
            recommendationsTextBox.AppendText("\n\nمعلومات إضافية:");
            recommendationsTextBox.AppendText($"\n• تم حفظ هذا التنبؤ في سجلك الشخصي");
            recommendationsTextBox.AppendText($"\n• دقة النموذج: 98.90%");
            recommendationsTextBox.AppendText($"\n• تاريخ التنبؤ: {DateTime.Now:yyyy-MM-dd HH:mm}");
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("هل تريد تسجيل الخروج؟", "تأكيد", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                var loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
            base.OnFormClosing(e);
        }
    }
}