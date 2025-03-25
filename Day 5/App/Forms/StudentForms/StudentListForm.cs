using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace App.Forms.StudentForms
{
    public partial class StudentListForm : Form
    {
        private DataGridView dgvStudents;

        public StudentListForm()
        {
            // Bật DoubleBuffered để hạn chế nháy (flicker) khi vẽ
            this.DoubleBuffered = true;

            InitializeComponent();
            InitializeCustomComponents();
        }

        /// <summary>
        /// Vẽ nền gradient cho Form
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Tạo gradient từ trên xuống dưới
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(230, 230, 230), // Màu trên (xám nhạt)
                Color.White,                   // Màu dưới (trắng)
                90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Danh sách sinh viên";
            this.ClientSize = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Tạo DataGridView
            dgvStudents = new DataGridView();
            dgvStudents.Dock = DockStyle.Fill;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.MultiSelect = false;
            dgvStudents.ReadOnly = true;
            dgvStudents.BorderStyle = BorderStyle.None;

            // Cài đặt thanh cuộn: luôn hiển thị cả thanh ngang và dọc nếu cần
            dgvStudents.ScrollBars = ScrollBars.Both;

            // (1) Tùy chỉnh màu nền, font, v.v.
            dgvStudents.BackgroundColor = Color.White;
            dgvStudents.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvStudents.DefaultCellStyle.BackColor = Color.White;
            dgvStudents.DefaultCellStyle.ForeColor = Color.Black;
            dgvStudents.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215); // Màu xanh Windows 10
            dgvStudents.DefaultCellStyle.SelectionForeColor = Color.White;

            // (2) Tạo hiệu ứng màu xen kẽ cho từng dòng
            dgvStudents.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            // (3) Tùy chỉnh tiêu đề cột (header)
            dgvStudents.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvStudents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvStudents.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStudents.EnableHeadersVisualStyles = false; // Để có hiệu lực màu custom

            // (4) Tùy chỉnh viền
            dgvStudents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvStudents.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvStudents.RowHeadersVisible = false; // Ẩn cột đầu dòng (nếu muốn)

            // Thêm các cột
            dgvStudents.Columns.Add("colId", "ID");
            dgvStudents.Columns.Add("colStudentId", "Mã SV");
            dgvStudents.Columns.Add("colFirstName", "Tên");
            dgvStudents.Columns.Add("colLastName", "Họ");
            dgvStudents.Columns.Add("colBirthDate", "Ngày sinh");
            dgvStudents.Columns.Add("colGender", "Giới tính");
            dgvStudents.Columns.Add("colPhoneNumber", "Số ĐT");
            dgvStudents.Columns.Add("colAddress", "Địa chỉ");
            dgvStudents.Columns.Add("colPicture", "Ảnh");

            // Demo: add sample rows
            dgvStudents.Rows.Add("1", "SV001", "Văn A", "Nguyễn", "2000-01-01", "Nam", "0123456789", "Hà Nội", "No Image");
            dgvStudents.Rows.Add("2", "SV002", "Thị B", "Trần", "2001-05-10", "Nữ", "0987654321", "TP. HCM", "Has Image");

            this.Controls.Add(dgvStudents);
        }
    }
}
