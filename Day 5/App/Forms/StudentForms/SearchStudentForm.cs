using System;
using System.Drawing;
using System.Windows.Forms;

namespace App.Forms.StudentForms
{
    public partial class SearchStudentForm : Form
    {
        private Label lblKeyword;
        private TextBox txtKeyword;
        private Button btnSearch;
        private DataGridView dgvResults;

        public SearchStudentForm()
        {
            this.DoubleBuffered = true;
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Tìm kiếm sinh viên";
            this.ClientSize = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            lblKeyword = new Label() { Text = "Nhập từ khóa (Mã SV / Tên):", Location = new Point(20, 20), AutoSize = true };
            txtKeyword = new TextBox() { Location = new Point(250, 18), Width = 300 };
            btnSearch = new Button() { Text = "Tìm kiếm", Location = new Point(570, 16), Width = 100 };
            btnSearch.Click += BtnSearch_Click;

            dgvResults = new DataGridView()
            {
                Location = new Point(20, 60),
                Size = new Size(850, 400),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true
            };

            // Thêm các cột
            dgvResults.Columns.Add("colId", "ID");
            dgvResults.Columns.Add("colStudentId", "Mã SV");
            dgvResults.Columns.Add("colFirstName", "Tên");
            dgvResults.Columns.Add("colLastName", "Họ");
            dgvResults.Columns.Add("colBirthDate", "Ngày sinh");
            dgvResults.Columns.Add("colGender", "Giới tính");
            dgvResults.Columns.Add("colPhoneNumber", "Số ĐT");
            dgvResults.Columns.Add("colAddress", "Địa chỉ");
            dgvResults.Columns.Add("colPicture", "Ảnh");

            this.Controls.Add(lblKeyword);
            this.Controls.Add(txtKeyword);
            this.Controls.Add(btnSearch);
            this.Controls.Add(dgvResults);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            dgvResults.Rows.Clear();
            if (!string.IsNullOrEmpty(txtKeyword.Text))
            {
                // Demo: hiển thị 1 kết quả mẫu
                dgvResults.Rows.Add("1", "SV001", "Văn A", "Nguyễn", "2000-01-01", "Nam", "0123456789", "Hà Nội", "No Image");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ khóa để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
