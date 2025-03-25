using App.Controllers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace App.Forms.StudentForms
{
    public partial class ExportStudentListForm : Form
    {
        private StudentController _studentController = new StudentController();
        private DataGridView dgvStudents;
        private Button btnExport;

        public ExportStudentListForm()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            var students = _studentController.GetAllStudents();

            this.Text = "Xuất danh sách sinh viên";
            this.ClientSize = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            dgvStudents = new DataGridView()
            {
                Dock = DockStyle.Top,
                Height = 400,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true
            };

            dgvStudents.Columns.Add("colId", "ID");
            dgvStudents.Columns.Add("colStudentId", "Mã SV");
            dgvStudents.Columns.Add("colFirstName", "Tên");
            dgvStudents.Columns.Add("colLastName", "Họ");
            dgvStudents.Columns.Add("colBirthDate", "Ngày sinh");
            dgvStudents.Columns.Add("colGender", "Giới tính");
            dgvStudents.Columns.Add("colPhoneNumber", "Số ĐT");
            dgvStudents.Columns.Add("colAddress", "Địa chỉ");
            dgvStudents.Columns.Add("colPicture", "Ảnh");

            foreach (var student in students)
            {
                dgvStudents.Rows.Add(
                    student.Id,
                    student.StudentId,
                    student.FirstName,
                    student.LastName,
                    student.BirthDate.ToString("yyyy-MM-dd"),
                    student.Gender,
                    student.PhoneNumber,
                    student.Address,
                    student.Picture != null ? "Has Image" : "No Image"
                );
            }

            btnExport = new Button()
            {
                Text = "Xuất danh sách",
                Size = new Size(120, 35),
                Location = new Point(20, 420)
            };
            btnExport.Click += BtnExport_Click;

            this.Controls.Add(dgvStudents);
            this.Controls.Add(btnExport);
        }


        private void BtnExport_Click(object sender, EventArgs e)
        {
            // TODO: Thực hiện logic xuất danh sách (Excel, CSV, PDF, …)
            this.Close();
        }
    }
}
