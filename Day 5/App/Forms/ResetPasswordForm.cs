using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using App.Controllers;

namespace App.Forms
{
    public partial class ResetPasswordForm : Form
    {
        private AccountController _accountController;
        private string generatedOTP = "";

        // UI Controls
        private Panel mainPanel;
        private Label lblTitle;

        private GroupBox grpOTP;
        private TableLayoutPanel tblOTP;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblOTP;
        private TextBox txtOTP;
        private FlowLayoutPanel otpButtonPanel;
        private Button btnSendOTP;
        private Button btnVerifyOTP;

        private GroupBox grpNewPassword;
        private TableLayoutPanel tblNewPassword;
        private Label lblNewPassword;
        private TextBox txtNewPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;

        private FlowLayoutPanel finalButtonPanel;
        private Button btnResetPassword;

        public ResetPasswordForm()
        {
            _accountController = new AccountController();
            this.DoubleBuffered = true;
            InitializeCustomComponents();
        }

        // Vẽ nền gradient cho Form
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(45, 45, 48),
                Color.FromArgb(25, 25, 28),
                90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void InitializeCustomComponents()
        {
            // Form settings
            this.Text = "Reset Password";
            this.ClientSize = new Size(450, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Main Panel
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

            // Title Label
            lblTitle = new Label();
            lblTitle.Text = "Reset Password";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkSlateBlue;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 60;

            // GroupBox OTP Verification
            grpOTP = new GroupBox();
            grpOTP.Text = "OTP Verification";
            grpOTP.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grpOTP.ForeColor = Color.DarkSlateBlue;
            grpOTP.Size = new Size(mainPanel.Width - 40, 160);
            grpOTP.Location = new Point(20, lblTitle.Height + 10);
            grpOTP.Paint += (s, e) =>
            {
                using (GraphicsPath path = GetRoundedRect(grpOTP.ClientRectangle, 10))
                {
                    grpOTP.Region = new Region(path);
                }
            };

            // TableLayoutPanel cho OTP: Email và OTP
            tblOTP = new TableLayoutPanel();
            tblOTP.ColumnCount = 2;
            tblOTP.RowCount = 2;
            tblOTP.Dock = DockStyle.Fill;
            tblOTP.Padding = new Padding(10);
            tblOTP.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            tblOTP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            tblOTP.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            tblOTP.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));

            lblEmail = new Label() { Text = "Email:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };
            txtEmail = new TextBox() { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };
            lblOTP = new Label() { Text = "OTP:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };
            txtOTP = new TextBox() { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };

            tblOTP.Controls.Add(lblEmail, 0, 0);
            tblOTP.Controls.Add(txtEmail, 1, 0);
            tblOTP.Controls.Add(lblOTP, 0, 1);
            tblOTP.Controls.Add(txtOTP, 1, 1);

            grpOTP.Controls.Add(tblOTP);

            // FlowLayoutPanel cho các nút OTP
            otpButtonPanel = new FlowLayoutPanel();
            otpButtonPanel.FlowDirection = FlowDirection.LeftToRight;
            otpButtonPanel.Dock = DockStyle.Bottom;
            otpButtonPanel.Padding = new Padding(10);
            otpButtonPanel.AutoSize = true;

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

            otpButtonPanel.Controls.Add(btnSendOTP);
            otpButtonPanel.Controls.Add(btnVerifyOTP);
            grpOTP.Controls.Add(otpButtonPanel);
            otpButtonPanel.BringToFront();

            // GroupBox New Password
            grpNewPassword = new GroupBox();
            grpNewPassword.Text = "New Password";
            grpNewPassword.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grpNewPassword.ForeColor = Color.DarkSlateBlue;
            grpNewPassword.Size = new Size(mainPanel.Width - 40, 150);
            grpNewPassword.Location = new Point(20, grpOTP.Bottom + 10);
            grpNewPassword.Paint += (s, e) =>
            {
                using (GraphicsPath path = GetRoundedRect(grpNewPassword.ClientRectangle, 10))
                {
                    grpNewPassword.Region = new Region(path);
                }
            };

            // TableLayoutPanel cho New Password và Confirm Password
            tblNewPassword = new TableLayoutPanel();
            tblNewPassword.ColumnCount = 2;
            tblNewPassword.RowCount = 2;
            tblNewPassword.Dock = DockStyle.Fill;
            tblNewPassword.Padding = new Padding(10);
            tblNewPassword.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
            tblNewPassword.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            tblNewPassword.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            tblNewPassword.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));

            lblNewPassword = new Label() { Text = "New Password:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };
            txtNewPassword = new TextBox() { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10), PasswordChar = '*', Enabled = false };
            lblConfirmPassword = new Label() { Text = "Confirm Password:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };
            txtConfirmPassword = new TextBox() { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10), PasswordChar = '*', Enabled = false };

            tblNewPassword.Controls.Add(lblNewPassword, 0, 0);
            tblNewPassword.Controls.Add(txtNewPassword, 1, 0);
            tblNewPassword.Controls.Add(lblConfirmPassword, 0, 1);
            tblNewPassword.Controls.Add(txtConfirmPassword, 1, 1);

            grpNewPassword.Controls.Add(tblNewPassword);

            // Final button panel for Reset Password
            finalButtonPanel = new FlowLayoutPanel();
            finalButtonPanel.FlowDirection = FlowDirection.LeftToRight;
            finalButtonPanel.Dock = DockStyle.Bottom;
            finalButtonPanel.Padding = new Padding(10);
            finalButtonPanel.AutoSize = true;

            btnResetPassword = new Button()
            {
                Text = "Reset Password",
                Font = new Font("Segoe UI", 10),
                BackColor = Color.OrangeRed,
                ForeColor = Color.White,
                AutoSize = true,
                Enabled = false
            };
            btnResetPassword.Click += BtnResetPassword_Click;

            finalButtonPanel.Controls.Add(btnResetPassword);

            // Thêm các control vào mainPanel
            mainPanel.Controls.Add(lblTitle);
            mainPanel.Controls.Add(grpOTP);
            mainPanel.Controls.Add(grpNewPassword);
            mainPanel.Controls.Add(finalButtonPanel);

            // Sắp xếp vị trí các control
            lblTitle.BringToFront();
            grpOTP.Location = new Point(20, lblTitle.Height + 10);
            grpNewPassword.Location = new Point(20, grpOTP.Bottom + 10);
            finalButtonPanel.Location = new Point((mainPanel.Width - finalButtonPanel.Width) / 2, grpNewPassword.Bottom + 10);

            // Thêm mainPanel vào Form
            this.Controls.Add(mainPanel);
        }

        // Utility: Tạo GraphicsPath bo tròn góc cho Rectangle
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

        // ================== Event Handlers ==================
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
                    Subject = "Reset Password OTP",
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
            // Kiểm tra nếu OTP chưa được gửi
            if (string.IsNullOrEmpty(generatedOTP))
            {
                MessageBox.Show("OTP has not been sent. Please request OTP first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtOTP.Text.Trim() == generatedOTP)
            {
                MessageBox.Show("OTP verified successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Kích hoạt các ô nhập mật khẩu mới
                txtNewPassword.Enabled = true;
                txtConfirmPassword.Enabled = true;
                btnResetPassword.Enabled = true;
            }
            else
            {
                MessageBox.Show("Invalid OTP. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnResetPassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please enter and confirm your new password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Sử dụng dummy accountId = 1 cho demo
            int dummyAccountId = 1;
            _accountController.ResetPassword(dummyAccountId, newPassword);
            MessageBox.Show("Password has been reset successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
