using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using App.Controllers;
using App.Models;

namespace App.Forms
{
    public partial class LoginForm : Form
    {
        private AccountController _accountController;

        // UI Controls
        private Panel mainPanel;
        private Label lblTitle;
        private PictureBox logoPicture;
        private GroupBox grpLogin;
        private TableLayoutPanel tblLogin;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private FlowLayoutPanel buttonPanel;
        private Button btnLogin;
        private Button btnSignUp;
        private Button btnResetPassword;

        public LoginForm()
        {
            this.DoubleBuffered = true; // Giảm flicker khi vẽ
            _accountController = new AccountController();
            InitializeCustomComponents();
        }

        /// <summary>
        /// Vẽ nền gradient cho Form
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(45, 45, 48), // Màu trên
                Color.FromArgb(25, 25, 28), // Màu dưới
                90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void InitializeCustomComponents()
        {
            // Form settings
            this.Text = "Login";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(450, 550);
            this.MaximizeBox = false;

            // Panel chính chứa toàn bộ nội dung
            mainPanel = new Panel();
            mainPanel.Size = new Size(400, 500);
            mainPanel.Location = new Point(
                (this.ClientSize.Width - mainPanel.Width) / 2,
                (this.ClientSize.Height - mainPanel.Height) / 2
            );
            mainPanel.BackColor = Color.White;
            mainPanel.Paint += (s, e) =>
            {
                using (GraphicsPath path = GetRoundedRect(mainPanel.ClientRectangle, 20))
                {
                    mainPanel.Region = new Region(path);
                }
            };

            // Tiêu đề
            lblTitle = new Label();
            lblTitle.Text = "Welcome to MyApp";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkSlateBlue;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 60;

            // Logo (tuỳ chọn)
            logoPicture = new PictureBox();
            logoPicture.ImageLocation = "https://via.placeholder.com/120";
            logoPicture.SizeMode = PictureBoxSizeMode.Zoom;
            logoPicture.Size = new Size(120, 120);
            // Đặt logo ở giữa panel, ngay dưới tiêu đề
            logoPicture.Location = new Point(
                (mainPanel.Width - logoPicture.Width) / 2,
                lblTitle.Height + 10
            );

            // GroupBox Login
            grpLogin = new GroupBox();
            grpLogin.Text = "Login Credentials";
            grpLogin.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grpLogin.ForeColor = Color.DarkSlateBlue;
            grpLogin.Size = new Size(mainPanel.Width - 40, 150);
            grpLogin.Location = new Point(20, logoPicture.Bottom + 10);
            grpLogin.Paint += (s, e) =>
            {
                using (GraphicsPath path = GetRoundedRect(grpLogin.ClientRectangle, 10))
                {
                    grpLogin.Region = new Region(path);
                }
            };

            // TableLayoutPanel cho Username & Password
            tblLogin = new TableLayoutPanel();
            tblLogin.ColumnCount = 2;
            tblLogin.RowCount = 2;
            tblLogin.Dock = DockStyle.Fill;
            tblLogin.Padding = new Padding(10);
            tblLogin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            tblLogin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 2; i++)
            {
                tblLogin.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            }

            lblUsername = new Label()
            {
                Text = "Username:",
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            txtUsername = new TextBox()
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };

            lblPassword = new Label()
            {
                Text = "Password:",
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            txtPassword = new TextBox()
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                PasswordChar = '*'
            };

            tblLogin.Controls.Add(lblUsername, 0, 0);
            tblLogin.Controls.Add(txtUsername, 1, 0);
            tblLogin.Controls.Add(lblPassword, 0, 1);
            tblLogin.Controls.Add(txtPassword, 1, 1);

            grpLogin.Controls.Add(tblLogin);

            // FlowLayoutPanel cho các nút
            buttonPanel = new FlowLayoutPanel();
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.FlowDirection = FlowDirection.LeftToRight;
            buttonPanel.Padding = new Padding(10);
            buttonPanel.AutoSize = true;

            btnLogin = new Button()
            {
                Text = "Login",
                Font = new Font("Segoe UI", 10),
                BackColor = Color.DarkSlateBlue,
                ForeColor = Color.White,
                AutoSize = true
            };
            btnLogin.Click += BtnLogin_Click;

            btnSignUp = new Button()
            {
                Text = "Sign Up",
                Font = new Font("Segoe UI", 10),
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                AutoSize = true
            };
            btnSignUp.Click += BtnSignUp_Click;

            btnResetPassword = new Button()
            {
                Text = "Reset Password",
                Font = new Font("Segoe UI", 10),
                BackColor = Color.OrangeRed,
                ForeColor = Color.White,
                AutoSize = true
            };
            btnResetPassword.Click += BtnResetPassword_Click;

            buttonPanel.Controls.Add(btnLogin);
            buttonPanel.Controls.Add(btnSignUp);
            buttonPanel.Controls.Add(btnResetPassword);

            // Thêm control vào mainPanel
            mainPanel.Controls.Add(lblTitle);
            mainPanel.Controls.Add(logoPicture);
            mainPanel.Controls.Add(grpLogin);
            mainPanel.Controls.Add(buttonPanel);

            // Tính toán vị trí buttonPanel (bên dưới groupbox)
            buttonPanel.Location = new Point(
                (mainPanel.Width - buttonPanel.Width) / 2,
                grpLogin.Bottom + 20
            );

            // Thêm mainPanel vào Form
            this.Controls.Add(mainPanel);
        }

        /// <summary>
        /// Bo tròn góc cho panel/groupbox
        /// </summary>
        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        // ================= Event Handlers =================

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            Account account = _accountController.Login(username, password);
            if (account != null)
            {
                string currentUser = account.Username;
                ManagementForm managementForm = new ManagementForm(currentUser);
                this.Hide();
                managementForm.ShowDialog();
                this.Close();

            }
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            using (var signupForm = new SignupForm())
            {
                signupForm.ShowDialog();
            }
        }

        private void BtnResetPassword_Click(object sender, EventArgs e)
        {
            using (var resetForm = new ResetPasswordForm())
            {
                resetForm.ShowDialog();
            }
        }
    }
}
