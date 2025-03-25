using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using App.Controllers;
using App.Models;

namespace App.Forms
{
    public partial class SignupForm : Form
    {
        private AccountController _accountController;
        private string generatedOTP = "";

        // Controls
        private Panel mainPanel;
        private Label lblTitle;
        private GroupBox grpAccountInfo;
        private TableLayoutPanel tblAccount;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;

        private GroupBox grpOTP;
        private TableLayoutPanel tblOTP;
        private Label lblOTP;
        private TextBox txtOTP;

        private FlowLayoutPanel buttonPanel;
        private Button btnSendOTP;
        private Button btnVerifyOTP;
        private Button btnSignUp;

        public SignupForm()
        {
            _accountController = new AccountController();
            this.DoubleBuffered = true;  // Giảm hiện tượng nháy
            InitializeCustomComponents();
        }

        // Vẽ nền gradient cho form
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(45, 45, 48),   // Màu trên
                Color.FromArgb(25, 25, 28),   // Màu dưới
                90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void InitializeCustomComponents()
        {
            // Form settings
            this.Text = "Sign Up";
            this.ClientSize = new Size(450, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Panel chính chứa toàn bộ nội dung
            mainPanel = new Panel();
            mainPanel.Size = new Size(400, 500);
            mainPanel.Location = new Point((this.ClientSize.Width - mainPanel.Width) / 2,
                                           (this.ClientSize.Height - mainPanel.Height) / 2);
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
            lblTitle.Text = "Create Account";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkSlateBlue;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 60;

            // GroupBox thông tin tài khoản
            grpAccountInfo = new GroupBox();
            grpAccountInfo.Text = "Account Information";
            grpAccountInfo.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grpAccountInfo.ForeColor = Color.DarkSlateBlue;
            grpAccountInfo.Size = new Size(mainPanel.Width - 40, 160);
            grpAccountInfo.Location = new Point(20, lblTitle.Height + 10);
            grpAccountInfo.Paint += (s, e) =>
            {
                // Bo tròn groupbox (tùy chọn, có thể bỏ qua nếu muốn giữ khung mặc định)
                using (GraphicsPath path = GetRoundedRect(grpAccountInfo.ClientRectangle, 10))
                {
                    grpAccountInfo.Region = new Region(path);
                }
            };

            // Bảng sắp xếp label - textbox cho tài khoản
            tblAccount = new TableLayoutPanel();
            tblAccount.ColumnCount = 2;
            tblAccount.RowCount = 3;
            tblAccount.Dock = DockStyle.Fill;
            tblAccount.Padding = new Padding(10);
            tblAccount.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            tblAccount.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 3; i++)
            {
                tblAccount.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            }

            // Email
            lblEmail = new Label() { Text = "Email:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtEmail = new TextBox() { Dock = DockStyle.Fill };
            tblAccount.Controls.Add(lblEmail, 0, 0);
            tblAccount.Controls.Add(txtEmail, 1, 0);

            // Password
            lblPassword = new Label() { Text = "Password:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtPassword = new TextBox() { Dock = DockStyle.Fill, PasswordChar = '*' };
            tblAccount.Controls.Add(lblPassword, 0, 1);
            tblAccount.Controls.Add(txtPassword, 1, 1);

            // Confirm Password
            lblConfirmPassword = new Label() { Text = "Confirm:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtConfirmPassword = new TextBox() { Dock = DockStyle.Fill, PasswordChar = '*' };
            tblAccount.Controls.Add(lblConfirmPassword, 0, 2);
            tblAccount.Controls.Add(txtConfirmPassword, 1, 2);

            grpAccountInfo.Controls.Add(tblAccount);

            // GroupBox OTP
            grpOTP = new GroupBox();
            grpOTP.Text = "OTP Verification";
            grpOTP.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grpOTP.ForeColor = Color.DarkSlateBlue;
            grpOTP.Size = new Size(mainPanel.Width - 40, 100);
            grpOTP.Location = new Point(20, grpAccountInfo.Bottom + 10);
            grpOTP.Paint += (s, e) =>
            {
                using (GraphicsPath path = GetRoundedRect(grpOTP.ClientRectangle, 10))
                {
                    grpOTP.Region = new Region(path);
                }
            };

            // Bảng sắp xếp cho OTP
            tblOTP = new TableLayoutPanel();
            tblOTP.ColumnCount = 2;
            tblOTP.RowCount = 1;
            tblOTP.Dock = DockStyle.Fill;
            tblOTP.Padding = new Padding(10);
            tblOTP.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
            tblOTP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            tblOTP.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));

            lblOTP = new Label() { Text = "OTP:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtOTP = new TextBox() { Dock = DockStyle.Fill };
            tblOTP.Controls.Add(lblOTP, 0, 0);
            tblOTP.Controls.Add(txtOTP, 1, 0);

            grpOTP.Controls.Add(tblOTP);

            // FlowLayoutPanel cho các nút
            buttonPanel = new FlowLayoutPanel();
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.FlowDirection = FlowDirection.LeftToRight;
            buttonPanel.Padding = new Padding(10);
            buttonPanel.AutoSize = true;

            btnSendOTP = new Button()
            {
                Text = "Send OTP",
                Font = new Font("Segoe UI", 10),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                AutoSize = true
            };
            btnSendOTP.Click += BtnSendOTP_Click;

            btnVerifyOTP = new Button()
            {
                Text = "Verify OTP",
                Font = new Font("Segoe UI", 10),
                BackColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                AutoSize = true
            };
            btnVerifyOTP.Click += BtnVerifyOTP_Click;

            btnSignUp = new Button()
            {
                Text = "Sign Up",
                Font = new Font("Segoe UI", 10),
                BackColor = Color.OrangeRed,
                ForeColor = Color.White,
                AutoSize = true,
                Enabled = false
            };
            btnSignUp.Click += BtnSignUp_Click;

            buttonPanel.Controls.Add(btnSendOTP);
            buttonPanel.Controls.Add(btnVerifyOTP);
            buttonPanel.Controls.Add(btnSignUp);

            // Thêm các control vào panel
            mainPanel.Controls.Add(lblTitle);
            mainPanel.Controls.Add(grpAccountInfo);
            mainPanel.Controls.Add(grpOTP);
            mainPanel.Controls.Add(buttonPanel);

            // Sắp xếp vị trí cho buttonPanel
            buttonPanel.Location = new Point(
                (mainPanel.Width - buttonPanel.Width) / 2,
                grpOTP.Bottom + 10
            );

            // Thêm panel vào Form
            this.Controls.Add(mainPanel);
        }

        // Bo tròn góc cho Panel/GroupBox
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

        // ============== Event Handlers ==============
        private void BtnSendOTP_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter your email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            generatedOTP = new Random().Next(100000, 999999).ToString();
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("yourEmail@gmail.com", "yourEmailPassword"),
                    EnableSsl = true,
                };
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("yourEmail@gmail.com"),
                    Subject = "Signup OTP Code",
                    Body = $"Your OTP code is: {generatedOTP}",
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(email);
                smtpClient.Send(mailMessage);
                MessageBox.Show("OTP sent to your email.", "OTP Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending OTP: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVerifyOTP_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu OTP chưa được gửi (generatedOTP rỗng)
            if (string.IsNullOrEmpty(generatedOTP))
            {
                MessageBox.Show("OTP has not been sent. Please request OTP first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtOTP.Text.Trim() == generatedOTP)
            {
                MessageBox.Show("OTP verified successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSignUp.Enabled = true;
            }
            else
            {
                MessageBox.Show("Invalid OTP. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Account newAccount = new Account
            {
                Email = email,
                Password = password
                // Username nếu cần, có thể bổ sung
            };

            _accountController.SignUp(newAccount);
            MessageBox.Show("Account created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
