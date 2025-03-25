using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace App.Forms.StudentForms
{
    public partial class AddStudentForm : Form
    {
        private Label lblStudentId, lblFirstName, lblLastName, lblBirthDate, lblGender, lblPhoneNumber, lblAddress, lblPicture;
        private TextBox txtStudentId, txtFirstName, txtLastName, txtPhoneNumber, txtAddress;
        private DateTimePicker dtpBirthDate;
        private ComboBox cbGender;
        private PictureBox picStudent;
        private Button btnChooseImage, btnAdd, btnImportData;
        private byte[] studentImageData; // Lưu ảnh dưới dạng byte[]

        public AddStudentForm()
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
            this.Text = "Thêm mới sinh viên";
            this.ClientSize = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Sử dụng TableLayoutPanel để căn chỉnh các control
            TableLayoutPanel tbl = new TableLayoutPanel();
            tbl.ColumnCount = 2;
            tbl.RowCount = 10; // Tăng số hàng lên 10 để thêm nút Import Data
            tbl.Dock = DockStyle.Fill;
            tbl.Padding = new Padding(20);
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 10; i++)
                tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            // Row 0: Mã SV
            lblStudentId = new Label() { Text = "Mã SV:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtStudentId = new TextBox() { Dock = DockStyle.Fill };

            // Row 1: Tên
            lblFirstName = new Label() { Text = "Tên:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtFirstName = new TextBox() { Dock = DockStyle.Fill };

            // Row 2: Họ
            lblLastName = new Label() { Text = "Họ:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtLastName = new TextBox() { Dock = DockStyle.Fill };

            // Row 3: Ngày sinh
            lblBirthDate = new Label() { Text = "Ngày sinh:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            dtpBirthDate = new DateTimePicker() { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short };

            // Row 4: Giới tính
            lblGender = new Label() { Text = "Giới tính:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            cbGender = new ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbGender.Items.AddRange(new string[] { "Nam", "Nữ", "Khác" });
            cbGender.SelectedIndex = 0;

            // Row 5: Số ĐT
            lblPhoneNumber = new Label() { Text = "Số ĐT:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtPhoneNumber = new TextBox() { Dock = DockStyle.Fill };

            // Row 6: Địa chỉ
            lblAddress = new Label() { Text = "Địa chỉ:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtAddress = new TextBox() { Dock = DockStyle.Fill };

            // Row 7: Ảnh
            lblPicture = new Label() { Text = "Ảnh:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            picStudent = new PictureBox() { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle };
            // Tạo panel cho ảnh và nút chọn ảnh
            Panel picPanel = new Panel() { Dock = DockStyle.Fill };
            picStudent.Size = new Size(100, 100);
            picStudent.Location = new Point(0, 0);
            btnChooseImage = new Button() { Text = "Chọn ảnh", AutoSize = true };
            btnChooseImage.Click += BtnChooseImage_Click;
            btnChooseImage.Location = new Point(110, 35);
            picPanel.Controls.Add(picStudent);
            picPanel.Controls.Add(btnChooseImage);

            // Row 8: Nút Import Data (mới)
            btnImportData = new Button() { Text = "Import Data", AutoSize = true };
            btnImportData.Click += BtnImportData_Click;

            // Row 9: Nút Thêm sinh viên
            btnAdd = new Button() { Text = "Thêm sinh viên", AutoSize = true };
            btnAdd.Click += BtnAdd_Click;

            // Thêm control vào TableLayoutPanel theo thứ tự
            tbl.Controls.Add(lblStudentId, 0, 0);
            tbl.Controls.Add(txtStudentId, 1, 0);
            tbl.Controls.Add(lblFirstName, 0, 1);
            tbl.Controls.Add(txtFirstName, 1, 1);
            tbl.Controls.Add(lblLastName, 0, 2);
            tbl.Controls.Add(txtLastName, 1, 2);
            tbl.Controls.Add(lblBirthDate, 0, 3);
            tbl.Controls.Add(dtpBirthDate, 1, 3);
            tbl.Controls.Add(lblGender, 0, 4);
            tbl.Controls.Add(cbGender, 1, 4);
            tbl.Controls.Add(lblPhoneNumber, 0, 5);
            tbl.Controls.Add(txtPhoneNumber, 1, 5);
            tbl.Controls.Add(lblAddress, 0, 6);
            tbl.Controls.Add(txtAddress, 1, 6);
            tbl.Controls.Add(lblPicture, 0, 7);
            tbl.Controls.Add(picPanel, 1, 7);
            // Thêm nút Import Data ở hàng 8, span 2 cột
            tbl.SetColumnSpan(btnImportData, 2);
            tbl.Controls.Add(btnImportData, 0, 8);
            // Thêm nút Thêm sinh viên ở hàng 9, span 2 cột
            tbl.SetColumnSpan(btnAdd, 2);
            tbl.Controls.Add(btnAdd, 0, 9);

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
                    using (MemoryStream ms = new MemoryStream())
                    {
                        picStudent.Image.Save(ms, picStudent.Image.RawFormat);
                        studentImageData = ms.ToArray();
                    }
                }
            }
        }

        // Sự kiện cho nút Import Data
        private void BtnImportData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSV Files|*.csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    try
                    {
                        // Đọc tất cả các dòng từ file CSV
                        string[] lines = File.ReadAllLines(filePath);

                        if (lines.Length == 0)
                        {
                            MessageBox.Show("File CSV không có dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Ở đây, ta chỉ lấy dữ liệu của DÒNG ĐẦU TIÊN để đưa vào form
                        // Nếu bạn muốn xử lý nhiều dòng, bạn cần lặp qua mảng lines
                        string firstLine = lines[0];
                        string[] data = firstLine.Split(',');

                        // Kiểm tra xem có đủ số cột mong muốn hay không
                        // Giả sử bạn cần tối thiểu 9 cột như ví dụ
                        if (data.Length < 9)
                        {
                            MessageBox.Show("Dữ liệu CSV không đúng định dạng (thiếu cột).",
                                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // data[0] là ID (thường do DB tự tạo, có thể bỏ qua)
                        txtStudentId.Text = data[1];     // Mã SV
                        txtFirstName.Text = data[2];    // Tên
                        txtLastName.Text = data[3];     // Họ

                        // Xử lý ngày sinh
                        DateTime birthDate;
                        if (DateTime.TryParse(data[4], out birthDate))
                        {
                            dtpBirthDate.Value = birthDate;
                        }
                        else
                        {
                            MessageBox.Show("Ngày sinh không hợp lệ.",
                                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        // Giới tính
                        string gender = data[5];
                        // Nếu comboBox của bạn có các lựa chọn "Nam", "Nữ", "Khác"
                        // Thì bạn kiểm tra xem CSV có chuỗi tương ứng không
                        if (cbGender.Items.Contains(gender))
                        {
                            cbGender.SelectedItem = gender;
                        }
                        else
                        {
                            // Mặc định về index 0 nếu không tìm thấy
                            cbGender.SelectedIndex = 0;
                        }

                        txtPhoneNumber.Text = data[6];  // Số ĐT
                        txtAddress.Text = data[7];      // Địa chỉ

                        // Giả sử data[8] là cột ảnh
                        if (!string.IsNullOrEmpty(data[8]) && File.Exists(data[8]))
                        {
                            picStudent.Image = Image.FromFile(data[8]);
                            using (MemoryStream ms = new MemoryStream())
                            {
                                picStudent.Image.Save(ms, picStudent.Image.RawFormat);
                                studentImageData = ms.ToArray();
                            }
                        }
                        else
                        {
                            // Nếu không có ảnh hoặc đường dẫn không tồn tại
                            picStudent.Image = null;
                            studentImageData = null;
                        }


                        MessageBox.Show("Import dữ liệu thành công!", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi đọc file CSV: " + ex.Message,
                                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ form
            string studentId = txtStudentId.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            DateTime birthDate = dtpBirthDate.Value;
            string gender = cbGender.SelectedItem.ToString();
            string phoneNumber = txtPhoneNumber.Text.Trim();
            string address = txtAddress.Text.Trim();
            byte[] pictureData = studentImageData;

            // TODO: Thực hiện logic lưu dữ liệu vào DB
            MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
