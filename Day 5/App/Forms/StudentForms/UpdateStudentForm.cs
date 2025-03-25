using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace App.Forms.StudentForms
{
    public partial class UpdateStudentForm : Form
    {
        private Label lblId, lblStudentId, lblFirstName, lblLastName, lblBirthDate, lblGender, lblPhoneNumber, lblAddress, lblPicture;
        private TextBox txtId, txtStudentId, txtFirstName, txtLastName, txtPhoneNumber, txtAddress;
        private DateTimePicker dtpBirthDate;
        private ComboBox cbGender;
        private PictureBox picStudent;
        private Button btnChooseImage, btnUpdate;
        private byte[] studentImageData;

        public UpdateStudentForm()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            InitializeCustomComponents();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(230, 230, 230),
                Color.White,
                90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Cập nhật thông tin sinh viên";
            this.ClientSize = new Size(500, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            TableLayoutPanel tbl = new TableLayoutPanel();
            tbl.ColumnCount = 2;
            tbl.RowCount = 10;
            tbl.Dock = DockStyle.Fill;
            tbl.Padding = new Padding(20);
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 10; i++)
                tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            lblId = new Label() { Text = "ID:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtId = new TextBox() { Dock = DockStyle.Fill, ReadOnly = true, Text = "1" };

            lblStudentId = new Label() { Text = "Mã SV:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtStudentId = new TextBox() { Dock = DockStyle.Fill, ReadOnly = true, Text = "SV001" };

            lblFirstName = new Label() { Text = "Tên:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtFirstName = new TextBox() { Dock = DockStyle.Fill, Text = "Văn A" };

            lblLastName = new Label() { Text = "Họ:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtLastName = new TextBox() { Dock = DockStyle.Fill, Text = "Nguyễn" };

            lblBirthDate = new Label() { Text = "Ngày sinh:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            dtpBirthDate = new DateTimePicker() { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short };
            dtpBirthDate.Value = new DateTime(2000, 1, 1);

            lblGender = new Label() { Text = "Giới tính:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            cbGender = new ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbGender.Items.AddRange(new string[] { "Nam", "Nữ", "Khác" });
            cbGender.SelectedIndex = 0;

            lblPhoneNumber = new Label() { Text = "Số ĐT:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtPhoneNumber = new TextBox() { Dock = DockStyle.Fill, Text = "0123456789" };

            lblAddress = new Label() { Text = "Địa chỉ:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtAddress = new TextBox() { Dock = DockStyle.Fill, Text = "Hà Nội" };

            lblPicture = new Label() { Text = "Ảnh:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };

            picStudent = new PictureBox() { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle };
            picStudent.Size = new Size(100, 100);
            btnChooseImage = new Button() { Text = "Chọn ảnh", AutoSize = true };
            btnChooseImage.Click += BtnChooseImage_Click;

            Panel picPanel = new Panel() { Dock = DockStyle.Fill };
            picPanel.Controls.Add(picStudent);
            picPanel.Controls.Add(btnChooseImage);
            btnChooseImage.Location = new Point(110, 35);

            // Thêm control vào TableLayoutPanel
            tbl.Controls.Add(lblId, 0, 0);
            tbl.Controls.Add(txtId, 1, 0);
            tbl.Controls.Add(lblStudentId, 0, 1);
            tbl.Controls.Add(txtStudentId, 1, 1);
            tbl.Controls.Add(lblFirstName, 0, 2);
            tbl.Controls.Add(txtFirstName, 1, 2);
            tbl.Controls.Add(lblLastName, 0, 3);
            tbl.Controls.Add(txtLastName, 1, 3);
            tbl.Controls.Add(lblBirthDate, 0, 4);
            tbl.Controls.Add(dtpBirthDate, 1, 4);
            tbl.Controls.Add(lblGender, 0, 5);
            tbl.Controls.Add(cbGender, 1, 5);
            tbl.Controls.Add(lblPhoneNumber, 0, 6);
            tbl.Controls.Add(txtPhoneNumber, 1, 6);
            tbl.Controls.Add(lblAddress, 0, 7);
            tbl.Controls.Add(txtAddress, 1, 7);
            tbl.Controls.Add(lblPicture, 0, 8);
            tbl.Controls.Add(picPanel, 1, 8);

            btnUpdate = new Button() { Text = "Cập nhật", AutoSize = true };
            btnUpdate.Click += BtnUpdate_Click;
            tbl.SetColumnSpan(btnUpdate, 2);
            tbl.Controls.Add(btnUpdate, 0, 9);

            this.Controls.Add(tbl);
        }

        private void BtnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picStudent.Image = Image.FromFile(ofd.FileName);
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        picStudent.Image.Save(ms, picStudent.Image.RawFormat);
                        studentImageData = ms.ToArray();
                    }
                }
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            // TODO: Cập nhật DB với dữ liệu thay đổi
            MessageBox.Show("Thông tin sinh viên được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
