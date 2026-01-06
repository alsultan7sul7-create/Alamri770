using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace StudentPerformanceApp
{
    public partial class RegisterForm : Form
    {
        private Panel mainPanel;
        private Panel registerPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private TextBox fullNameTextBox;
        private TextBox usernameTextBox;
        private TextBox emailTextBox;
        private TextBox passwordTextBox;
        private TextBox confirmPasswordTextBox;
        private Button registerButton;
        private Button backToLoginButton;
        private Label fullNameLabel;
        private Label usernameLabel;
        private Label emailLabel;
        private Label passwordLabel;
        private Label confirmPasswordLabel;

        public RegisterForm()
        {
            InitializeComponent();
            SetupUI();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1000, 800);
            this.Text = "نظام توقع أداء الطلاب - إنشاء حساب جديد";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 248, 255);
            
            this.ResumeLayout(false);
        }

        private void SetupUI()
        {
            // Main Panel
            mainPanel = new Panel
            {
                Size = new Size(1000, 800),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(240, 248, 255)
            };

            // Title
            titleLabel = new Label
            {
                Text = "إنشاء حساب جديد",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 112),
                Size = new Size(300, 50),
                Location = new Point(350, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Subtitle
            subtitleLabel = new Label
            {
                Text = "انضم إلى نظام توقع أداء الطلاب",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.FromArgb(70, 130, 180),
                Size = new Size(300, 30),
                Location = new Point(350, 100),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Register Panel
            registerPanel = new Panel
            {
                Size = new Size(450, 550),
                Location = new Point(275, 150),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            registerPanel.Paint += RegisterPanel_Paint;

            // Full Name Label
            fullNameLabel = new Label
            {
                Text = "الاسم الكامل:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 112),
                Size = new Size(120, 25),
                Location = new Point(30, 30),
                RightToLeft = RightToLeft.Yes
            };

            // Full Name TextBox
            fullNameTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(390, 35),
                Location = new Point(30, 60),
                BorderStyle = BorderStyle.FixedSingle,
                RightToLeft = RightToLeft.Yes
            };
            fullNameTextBox.Enter += TextBox_Enter;
            fullNameTextBox.Leave += TextBox_Leave;

            // Username Label
            usernameLabel = new Label
            {
                Text = "اسم المستخدم:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 112),
                Size = new Size(120, 25),
                Location = new Point(30, 110),
                RightToLeft = RightToLeft.Yes
            };

            // Username TextBox
            usernameTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(390, 35),
                Location = new Point(30, 140),
                BorderStyle = BorderStyle.FixedSingle,
                RightToLeft = RightToLeft.No
            };
            usernameTextBox.Enter += TextBox_Enter;
            usernameTextBox.Leave += TextBox_Leave;

            // Email Label
            emailLabel = new Label
            {
                Text = "البريد الإلكتروني:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 112),
                Size = new Size(150, 25),
                Location = new Point(30, 190),
                RightToLeft = RightToLeft.Yes
            };

            // Email TextBox
            emailTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(390, 35),
                Location = new Point(30, 220),
                BorderStyle = BorderStyle.FixedSingle,
                RightToLeft = RightToLeft.No
            };
            emailTextBox.Enter += TextBox_Enter;
            emailTextBox.Leave += TextBox_Leave;

            // Password Label
            passwordLabel = new Label
            {
                Text = "كلمة المرور:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 112),
                Size = new Size(120, 25),
                Location = new Point(30, 270),
                RightToLeft = RightToLeft.Yes
            };

            // Password TextBox
            passwordTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(390, 35),
                Location = new Point(30, 300),
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true,
                RightToLeft = RightToLeft.No
            };
            passwordTextBox.Enter += TextBox_Enter;
            passwordTextBox.Leave += TextBox_Leave;

            // Confirm Password Label
            confirmPasswordLabel = new Label
            {
                Text = "تأكيد كلمة المرور:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 112),
                Size = new Size(150, 25),
                Location = new Point(30, 350),
                RightToLeft = RightToLeft.Yes
            };

            // Confirm Password TextBox
            confirmPasswordTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(390, 35),
                Location = new Point(30, 380),
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true,
                RightToLeft = RightToLeft.No
            };
            confirmPasswordTextBox.Enter += TextBox_Enter;
            confirmPasswordTextBox.Leave += TextBox_Leave;

            // Register Button
            registerButton = new Button
            {
                Text = "إنشاء الحساب",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(390, 45),
                Location = new Point(30, 440),
                BackColor = Color.FromArgb(34, 139, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            registerButton.FlatAppearance.BorderSize = 0;
            registerButton.Click += RegisterButton_Click;
            registerButton.MouseEnter += RegisterButton_MouseEnter;
            registerButton.MouseLeave += RegisterButton_MouseLeave;

            // Back to Login Button
            backToLoginButton = new Button
            {
                Text = "العودة لتسجيل الدخول",
                Font = new Font("Segoe UI", 11),
                Size = new Size(390, 40),
                Location = new Point(30, 500),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(70, 130, 180),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            backToLoginButton.FlatAppearance.BorderColor = Color.FromArgb(70, 130, 180);
            backToLoginButton.FlatAppearance.BorderSize = 2;
            backToLoginButton.Click += BackToLoginButton_Click;
            backToLoginButton.MouseEnter += BackToLoginButton_MouseEnter;
            backToLoginButton.MouseLeave += BackToLoginButton_MouseLeave;

            // Add controls to register panel
            registerPanel.Controls.AddRange(new Control[] {
                fullNameLabel, fullNameTextBox, usernameLabel, usernameTextBox,
                emailLabel, emailTextBox, passwordLabel, passwordTextBox,
                confirmPasswordLabel, confirmPasswordTextBox, registerButton, backToLoginButton
            });

            // Add controls to main panel
            mainPanel.Controls.AddRange(new Control[] {
                titleLabel, subtitleLabel, registerPanel
            });

            // Add main panel to form
            this.Controls.Add(mainPanel);

            // Set tab order
            fullNameTextBox.TabIndex = 0;
            usernameTextBox.TabIndex = 1;
            emailTextBox.TabIndex = 2;
            passwordTextBox.TabIndex = 3;
            confirmPasswordTextBox.TabIndex = 4;
            registerButton.TabIndex = 5;

            // Enter key handling
            this.AcceptButton = registerButton;
        }

        private void RegisterPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            using (Pen pen = new Pen(Color.FromArgb(200, 200, 200), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.BackColor = Color.FromArgb(245, 250, 255);
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.BackColor = Color.White;
        }

        private void RegisterButton_MouseEnter(object sender, EventArgs e)
        {
            registerButton.BackColor = Color.FromArgb(50, 205, 50);
        }

        private void RegisterButton_MouseLeave(object sender, EventArgs e)
        {
            registerButton.BackColor = Color.FromArgb(34, 139, 34);
        }

        private void BackToLoginButton_MouseEnter(object sender, EventArgs e)
        {
            backToLoginButton.BackColor = Color.FromArgb(245, 250, 255);
        }

        private void BackToLoginButton_MouseLeave(object sender, EventArgs e)
        {
            backToLoginButton.BackColor = Color.Transparent;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            string fullName = fullNameTextBox.Text.Trim();
            string username = usernameTextBox.Text.Trim();
            string email = emailTextBox.Text.Trim();
            string password = passwordTextBox.Text;

            if (DatabaseManager.RegisterUser(username, email, password, fullName))
            {
                MessageBox.Show("تم إنشاء الحساب بنجاح!", "نجح التسجيل", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // تسجيل دخول تلقائي
                User? user = DatabaseManager.LoginUser(username, password);
                if (user != null)
                {
                    var dashboardForm = new DashboardForm(user);
                    dashboardForm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("اسم المستخدم أو البريد الإلكتروني مستخدم بالفعل", "خطأ في التسجيل", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(fullNameTextBox.Text))
            {
                MessageBox.Show("يرجى إدخال الاسم الكامل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fullNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
            {
                MessageBox.Show("يرجى إدخال اسم المستخدم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usernameTextBox.Focus();
                return false;
            }

            if (usernameTextBox.Text.Length < 3)
            {
                MessageBox.Show("اسم المستخدم يجب أن يكون 3 أحرف على الأقل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usernameTextBox.Focus();
                return false;
            }

            if (!IsValidEmail(emailTextBox.Text))
            {
                MessageBox.Show("يرجى إدخال بريد إلكتروني صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                emailTextBox.Focus();
                return false;
            }

            if (passwordTextBox.Text.Length < 6)
            {
                MessageBox.Show("كلمة المرور يجب أن تكون 6 أحرف على الأقل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                passwordTextBox.Focus();
                return false;
            }

            if (passwordTextBox.Text != confirmPasswordTextBox.Text)
            {
                MessageBox.Show("كلمات المرور غير متطابقة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                confirmPasswordTextBox.Focus();
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        private void BackToLoginButton_Click(object sender, EventArgs e)
        {
            var loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}