using System;
using System.Drawing;
using System.Windows.Forms;

namespace App.Forms.StudentForms
{
    public partial class DeleteStudentForm : Form
    {
        private Label lblStudentId;
        private TextBox txtStudentId;
        private Button btnDelete;

        public DeleteStudentForm()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Xóa sinh viên";
            this.ClientSize = new Size(400, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            TableLayoutPanel tbl = new TableLayoutPanel();
            tbl.ColumnCount = 1;
            tbl.RowCount = 3;
            tbl.Dock = DockStyle.Fill;
            tbl.Padding = new Padding(20);
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            lblStudentId = new Label() { Text = "Nhập Mã SV cần xóa:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            txtStudentId = new TextBox() { Dock = DockStyle.Fill };
            btnDelete = new Button() { Text = "Xóa", Dock = DockStyle.Fill };
            btnDelete.Click += BtnDelete_Click;

            tbl.Controls.Add(lblStudentId, 0, 0);
            tbl.Controls.Add(txtStudentId, 0, 1);
            tbl.Controls.Add(btnDelete, 0, 2);

            this.Controls.Add(tbl);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentId.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã SV!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // TODO: Thực hiện xóa trong DB
            MessageBox.Show($"Sinh viên có Mã SV {txtStudentId.Text} đã được xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
