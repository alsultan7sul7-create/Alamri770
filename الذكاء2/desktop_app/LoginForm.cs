using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentPerformanceApp
{
    public partial class LoginForm : Form
    {
        private Panel mainPanel;
        private Panel loginPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Button registerButton;
        private Label usernameLabel;
        private Label passwordLabel;
        private PictureBox logoBox;

        public LoginForm()
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
            this.ClientSize = new Size(1000, 700);
            this.Text = "نظام توقع أداء الطلاب - تسجيل الدخول";
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
                Size = new Size(1000, 700),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(240, 248, 255)
            };

            // Logo
            logoBox = new PictureBox
            {
                Size = new Size(100, 100),
                Location = new Point(450, 50),
                BackColor = Color.FromArgb(70, 130, 180),
                SizeMode = PictureBoxSizeMode.CenterImage
            };

            // Title
            titleLabel = new Label
            {
                Text = "نظام توقع أداء الطلاب",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 112),
                Size = new Size(400, 50),
                Location = new Point(300, 170),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Subtitle
            subtitleLabel = new Label
            {
                Text = "باستخدام خوارزمية الانحدار الخطي",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.FromArgb(70, 130, 180),
                Size = new Size(300, 30),
                Location = new Point(350, 220),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Login Panel
            loginPanel = new Panel
            {
                Size = new Size(400, 350),
                Location = new Point(300, 280),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            loginPanel.Paint += LoginPanel_Paint;

            // Username Label
            usernameLabel = new Label
            {
                Text = "اسم المستخدم:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 112),
                Size = new Size(120, 25),
                Location = new Point(30, 50),
                RightToLeft = RightToLeft.Yes
            };

            // Username TextBox
            usernameTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(340, 35),
                Location = new Point(30, 80),
                BorderStyle = BorderStyle.FixedSingle,
                RightToLeft = RightToLeft.No
            };
            usernameTextBox.Enter += TextBox_Enter;
            usernameTextBox.Leave += TextBox_Leave;

            // Password Label
            passwordLabel = new Label
            {
                Text = "كلمة المرور:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 112),
                Size = new Size(120, 25),
                Location = new Point(30, 130),
                RightToLeft = RightToLeft.Yes
            };

            // Password TextBox
            passwordTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(340, 35),
                Location = new Point(30, 160),
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true,
                RightToLeft = RightToLeft.No
            };
            passwordTextBox.Enter += TextBox_Enter;
            passwordTextBox.Leave += TextBox_Leave;

            // Login Button
            loginButton = new Button
            {
                Text = "تسجيل الدخول",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(340, 45),
                Location = new Point(30, 220),
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.Click += LoginButton_Click;
            loginButton.MouseEnter += Button_MouseEnter;
            loginButton.MouseLeave += Button_MouseLeave;

            // Register Button
            registerButton = new Button
            {
                Text = "إنشاء حساب جديد",
                Font = new Font("Segoe UI", 11),
                Size = new Size(340, 40),
                Location = new Point(30, 280),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(70, 130, 180),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            registerButton.FlatAppearance.BorderColor = Color.FromArgb(70, 130, 180);
            registerButton.FlatAppearance.BorderSize = 2;
            registerButton.Click += RegisterButton_Click;
            registerButton.MouseEnter += RegisterButton_MouseEnter;
            registerButton.MouseLeave += RegisterButton_MouseLeave;

            // Add controls to login panel
            loginPanel.Controls.AddRange(new Control[] {
                usernameLabel, usernameTextBox, passwordLabel, passwordTextBox,
                loginButton, registerButton
            });

            // Add controls to main panel
            mainPanel.Controls.AddRange(new Control[] {
                logoBox, titleLabel, subtitleLabel, loginPanel
            });

            // Add main panel to form
            this.Controls.Add(mainPanel);

            // Set tab order
            usernameTextBox.TabIndex = 0;
            passwordTextBox.TabIndex = 1;
            loginButton.TabIndex = 2;
            registerButton.TabIndex = 3;

            // Enter key handling
            this.AcceptButton = loginButton;
        }

        private void LoginPanel_Paint(object sender, PaintEventArgs e)
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

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.BackColor = Color.FromArgb(100, 149, 237);
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.BackColor = Color.FromArgb(70, 130, 180);
        }

        private void RegisterButton_MouseEnter(object sender, EventArgs e)
        {
            registerButton.BackColor = Color.FromArgb(245, 250, 255);
        }

        private void RegisterButton_MouseLeave(object sender, EventArgs e)
        {
            registerButton.BackColor = Color.Transparent;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("يرجى إدخال اسم المستخدم وكلمة المرور", "خطأ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User? user = DatabaseManager.LoginUser(username, password);
            if (user != null)
            {
                MessageBox.Show($"مرحباً {user.FullName}!", "تم تسجيل الدخول بنجاح", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                var dashboardForm = new DashboardForm(user);
                dashboardForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة", "خطأ في تسجيل الدخول", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            var registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }
    }
}