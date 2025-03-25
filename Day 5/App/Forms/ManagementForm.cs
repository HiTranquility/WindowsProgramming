using App.Forms.StudentForms;
using System;
using System.Drawing;
using System.Windows.Forms;
// (Nếu bạn cần gọi các form cụ thể, hãy thêm using App.Forms.StudentForms; v.v.)

namespace App.Forms
{
    public partial class ManagementForm : Form
    {
        private MenuStrip menuStrip;
        private Panel headerPanel;
        private Label lblCurrentUser;
        private Button btnLogout;

        // Main menu items
        private ToolStripMenuItem studentManagementMenuItem;
        private ToolStripMenuItem registrationApprovalMenuItem;
        private ToolStripMenuItem accountManagementMenuItem;
        private ToolStripMenuItem courseManagementMenuItem;
        private ToolStripMenuItem statisticsMenuItem;
        private ToolStripMenuItem settingsMenuItem;

        // Sub-menu items cho "Quản lý sinh viên"
        private ToolStripMenuItem studentListMenuItem;
        private ToolStripMenuItem addStudentMenuItem;
        private ToolStripMenuItem updateStudentMenuItem;
        private ToolStripMenuItem deleteStudentMenuItem;
        private ToolStripMenuItem searchStudentMenuItem;
        private ToolStripMenuItem exportStudentMenuItem;

        private string currentUsername;

        public ManagementForm(string username)
        {
            currentUsername = username;
            InitializeComponent();          // Nếu bạn dùng WinForms Designer
            InitializeCustomComponents();   // Hoặc gọi riêng để tạo giao diện bằng code
        }

        private void InitializeCustomComponents()
        {
            // 1) Form settings
            this.Text = "Management Dashboard";
            this.ClientSize = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            // 2) MenuStrip
            menuStrip = new MenuStrip();
            menuStrip.Dock = DockStyle.Top;
            menuStrip.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            menuStrip.BackColor = Color.FromArgb(50, 50, 50);
            menuStrip.ForeColor = Color.White;

            // --- Tạo các main menu items ---
            studentManagementMenuItem = new ToolStripMenuItem("Quản lý sinh viên");
            registrationApprovalMenuItem = new ToolStripMenuItem("Xét duyệt tài khoản đăng ký");
            accountManagementMenuItem = new ToolStripMenuItem("Quản lý tài khoản");
            courseManagementMenuItem = new ToolStripMenuItem("Quản lý khóa học");
            statisticsMenuItem = new ToolStripMenuItem("Thống kê");
            settingsMenuItem = new ToolStripMenuItem("Cài đặt");

            // 3) Sub-menu cho "Quản lý sinh viên"
            studentListMenuItem = new ToolStripMenuItem("Danh sách sinh viên");
            addStudentMenuItem = new ToolStripMenuItem("Thêm mới sinh viên");
            updateStudentMenuItem = new ToolStripMenuItem("Cập nhật sinh viên");
            deleteStudentMenuItem = new ToolStripMenuItem("Xóa sinh viên");
            searchStudentMenuItem = new ToolStripMenuItem("Tìm kiếm sinh viên");
            exportStudentMenuItem = new ToolStripMenuItem("Xuất danh sách sinh viên");

            // Thêm vào studentManagementMenuItem
            studentManagementMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                studentListMenuItem,
                addStudentMenuItem,
                updateStudentMenuItem,
                deleteStudentMenuItem,
                searchStudentMenuItem,
                exportStudentMenuItem
            });
            // Đăng ký sự kiện
            studentManagementMenuItem.DropDownItemClicked += StudentManagementMenuItem_DropDownItemClicked;

            // 4) Sub-menu cho "Xét duyệt tài khoản đăng ký" (demo)
            //    Có thể thêm "Danh sách chờ duyệt", "Xem chi tiết hồ sơ", "Phân quyền"...
            registrationApprovalMenuItem.DropDownItems.Add("Danh sách chờ duyệt").Click += (s, e) => MessageBox.Show("Hiển thị danh sách chờ duyệt...");
            registrationApprovalMenuItem.DropDownItems.Add("Xem chi tiết hồ sơ").Click += (s, e) => MessageBox.Show("Mở form xem chi tiết hồ sơ...");
            registrationApprovalMenuItem.DropDownItems.Add("Phân quyền").Click += (s, e) => MessageBox.Show("Mở form phân quyền...");

            // 5) Sub-menu cho "Quản lý tài khoản" (chia 2 nhóm: Admin & TK Sinh viên)

            // Nhóm Admin
            var adminListMenuItem = new ToolStripMenuItem("Danh sách Admin");
            var adminAddMenuItem = new ToolStripMenuItem("Thêm Admin");
            var adminUpdateMenuItem = new ToolStripMenuItem("Cập nhật Admin");
            var adminDeleteMenuItem = new ToolStripMenuItem("Xóa Admin");
            var adminSearchMenuItem = new ToolStripMenuItem("Tìm kiếm Admin");
            var adminExportMenuItem = new ToolStripMenuItem("Xuất danh sách Admin");

            // Nhóm Tài khoản Sinh viên
            var studentAccListMenuItem = new ToolStripMenuItem("Danh sách TK Sinh viên");
            var studentAccAddMenuItem = new ToolStripMenuItem("Thêm TK Sinh viên");
            var studentAccUpdateMenuItem = new ToolStripMenuItem("Cập nhật TK Sinh viên");
            var studentAccDeleteMenuItem = new ToolStripMenuItem("Xóa TK Sinh viên");
            var studentAccSearchMenuItem = new ToolStripMenuItem("Tìm kiếm TK Sinh viên");
            var studentAccExportMenuItem = new ToolStripMenuItem("Xuất danh sách TK Sinh viên");

            // Thêm vào "Quản lý tài khoản"
            accountManagementMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                adminListMenuItem,
                adminAddMenuItem,
                adminUpdateMenuItem,
                adminDeleteMenuItem,
                adminSearchMenuItem,
                adminExportMenuItem,
                new ToolStripSeparator(),
                studentAccListMenuItem,
                studentAccAddMenuItem,
                studentAccUpdateMenuItem,
                studentAccDeleteMenuItem,
                studentAccSearchMenuItem,
                studentAccExportMenuItem
            });
            // Đăng ký sự kiện
            accountManagementMenuItem.DropDownItemClicked += AccountManagementMenuItem_DropDownItemClicked;

            // 6) Sub-menu cho "Quản lý khóa học"
            var courseListMenuItem = new ToolStripMenuItem("Danh sách khóa học");
            var addCourseMenuItem = new ToolStripMenuItem("Thêm khóa học");
            var updateCourseMenuItem = new ToolStripMenuItem("Cập nhật khóa học");
            var deleteCourseMenuItem = new ToolStripMenuItem("Xóa khóa học");
            var searchCourseMenuItem = new ToolStripMenuItem("Tìm kiếm khóa học");
            var exportCourseMenuItem = new ToolStripMenuItem("Xuất danh sách khóa học");

            courseManagementMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                courseListMenuItem,
                addCourseMenuItem,
                updateCourseMenuItem,
                deleteCourseMenuItem,
                searchCourseMenuItem,
                exportCourseMenuItem
            });
            // Đăng ký sự kiện
            courseManagementMenuItem.DropDownItemClicked += CourseManagementMenuItem_DropDownItemClicked;

            // 7) Sub-menu cho "Thống kê" (demo)
            statisticsMenuItem.DropDownItems.Add("Thống kê sinh viên").Click += (s, e) => MessageBox.Show("Mở form thống kê sinh viên...");
            statisticsMenuItem.DropDownItems.Add("Thống kê khóa học").Click += (s, e) => MessageBox.Show("Mở form thống kê khóa học...");
            statisticsMenuItem.DropDownItems.Add("Xem biểu đồ").Click += (s, e) => MessageBox.Show("Mở form biểu đồ thống kê...");

            // 8) Sub-menu cho "Cài đặt" (demo)
            settingsMenuItem.DropDownItems.Add("Cấu hình hệ thống").Click += (s, e) => MessageBox.Show("Mở form cấu hình hệ thống...");
            settingsMenuItem.DropDownItems.Add("Thiết lập hệ thống").Click += (s, e) => MessageBox.Show("Mở form thiết lập hệ thống...");
            settingsMenuItem.DropDownItems.Add("Tùy chỉnh menu").Click += (s, e) => MessageBox.Show("Mở form tùy chỉnh menu...");

            // 9) Thêm tất cả main menu items vào MenuStrip
            menuStrip.Items.Add(studentManagementMenuItem);
            menuStrip.Items.Add(registrationApprovalMenuItem);
            menuStrip.Items.Add(accountManagementMenuItem);
            menuStrip.Items.Add(courseManagementMenuItem);
            menuStrip.Items.Add(statisticsMenuItem);
            menuStrip.Items.Add(settingsMenuItem);

            // 10) Panel header hiển thị thông tin đăng nhập & nút Logout
            headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 40;
            headerPanel.BackColor = Color.LightGray;

            lblCurrentUser = new Label();
            lblCurrentUser.Text = $"Bạn đang nhập với tư cách là: {currentUsername}";
            lblCurrentUser.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblCurrentUser.ForeColor = Color.Black;
            lblCurrentUser.AutoSize = true;
            lblCurrentUser.Location = new Point(10, 10);
            headerPanel.Controls.Add(lblCurrentUser);

            btnLogout = new Button();
            btnLogout.Text = "Logout";
            btnLogout.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            btnLogout.Size = new Size(80, 25);
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.Location = new Point(headerPanel.ClientSize.Width - btnLogout.Width - 10, 7);
            btnLogout.Click += BtnLogout_Click;
            headerPanel.Controls.Add(btnLogout);

            // Đảm bảo nút logout canh phải khi thay đổi kích thước
            headerPanel.Resize += (s, e) =>
            {
                btnLogout.Location = new Point(headerPanel.ClientSize.Width - btnLogout.Width - 10, 7);
            };

            // 11) Thêm MenuStrip và headerPanel vào Form
            this.Controls.Add(menuStrip);
            this.Controls.Add(headerPanel);
            this.MainMenuStrip = menuStrip;
        }

        // ----------- Sự kiện DropDownItemClicked cho "Quản lý sinh viên" -----------
        private void StudentManagementMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string selected = e.ClickedItem.Text;
            switch (selected)
            {
                case "Danh sách sinh viên":
                    // Ví dụ mở form StudentListForm
                    // using (var form = new StudentListForm()) { form.ShowDialog(); }
                    MessageBox.Show("Mở form Danh sách sinh viên...");
                    break;
                case "Thêm mới sinh viên":
                    // using (var form = new AddStudentForm()) { form.ShowDialog(); }
                    MessageBox.Show("Mở form Thêm mới sinh viên...");
                    break;
                case "Cập nhật sinh viên":
                    // using (var form = new UpdateStudentForm()) { form.ShowDialog(); }
                    MessageBox.Show("Mở form Cập nhật sinh viên...");
                    break;
                case "Xóa sinh viên":
                    // using (var form = new DeleteStudentForm()) { form.ShowDialog(); }
                    MessageBox.Show("Mở form Xóa sinh viên...");
                    break;
                case "Tìm kiếm sinh viên":
                    // using (var form = new SearchStudentForm()) { form.ShowDialog(); }
                    MessageBox.Show("Mở form Tìm kiếm sinh viên...");
                    break;
                case "Xuất danh sách sinh viên":
                    var form = new ExportStudentListForm();
                    form.Show();
                    break;
                default:
                    MessageBox.Show("Chức năng không xác định", "Thông báo");
                    break;
            }
        }

        // ----------- Sự kiện DropDownItemClicked cho "Quản lý tài khoản" -----------
        private void AccountManagementMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string selected = e.ClickedItem.Text;
            switch (selected)
            {
                // Nhóm Admin
                case "Danh sách Admin":
                    MessageBox.Show("Mở form Danh sách Admin...");
                    break;
                case "Thêm Admin":
                    MessageBox.Show("Mở form Thêm Admin...");
                    break;
                case "Cập nhật Admin":
                    MessageBox.Show("Mở form Cập nhật Admin...");
                    break;
                case "Xóa Admin":
                    MessageBox.Show("Mở form Xóa Admin...");
                    break;
                case "Tìm kiếm Admin":
                    MessageBox.Show("Mở form Tìm kiếm Admin...");
                    break;
                case "Xuất danh sách Admin":
                    MessageBox.Show("Mở form Xuất danh sách Admin...");
                    break;

                // Nhóm Tài khoản Sinh viên
                case "Danh sách TK Sinh viên":
                    MessageBox.Show("Mở form Danh sách TK Sinh viên...");
                    break;
                case "Thêm TK Sinh viên":
                    MessageBox.Show("Mở form Thêm TK Sinh viên...");
                    break;
                case "Cập nhật TK Sinh viên":
                    MessageBox.Show("Mở form Cập nhật TK Sinh viên...");
                    break;
                case "Xóa TK Sinh viên":
                    MessageBox.Show("Mở form Xóa TK Sinh viên...");
                    break;
                case "Tìm kiếm TK Sinh viên":
                    MessageBox.Show("Mở form Tìm kiếm TK Sinh viên...");
                    break;
                case "Xuất danh sách TK Sinh viên":
                    MessageBox.Show("Mở form Xuất danh sách TK Sinh viên...");
                    break;

                default:
                    MessageBox.Show("Chức năng không xác định", "Thông báo");
                    break;
            }
        }

        // ----------- Sự kiện DropDownItemClicked cho "Quản lý khóa học" -----------
        private void CourseManagementMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string selected = e.ClickedItem.Text;
            switch (selected)
            {
                case "Danh sách khóa học":
                    MessageBox.Show("Mở form Danh sách khóa học...");
                    break;
                case "Thêm khóa học":
                    MessageBox.Show("Mở form Thêm khóa học...");
                    break;
                case "Cập nhật khóa học":
                    MessageBox.Show("Mở form Cập nhật khóa học...");
                    break;
                case "Xóa khóa học":
                    MessageBox.Show("Mở form Xóa khóa học...");
                    break;
                case "Tìm kiếm khóa học":
                    MessageBox.Show("Mở form Tìm kiếm khóa học...");
                    break;
                case "Xuất danh sách khóa học":
                    MessageBox.Show("Mở form Xuất danh sách khóa học...");
                    break;
                default:
                    MessageBox.Show("Chức năng không xác định", "Thông báo");
                    break;
            }
        }

        // ----------- Nút Logout -----------
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
