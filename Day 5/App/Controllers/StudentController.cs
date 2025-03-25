using System;
using System.Collections.Generic;
using System.Windows.Forms;
using App.Models;
using App.Services;

namespace App.Controllers
{
    internal class StudentController
    {
        private readonly StudentSvc _studentService;

        public StudentController()
        {
            // Trong thực tế nên sử dụng Dependency Injection để inject StudentSvc
            _studentService = new StudentSvc();
        }

        /// <summary>
        /// Tạo mới sinh viên.
        /// </summary>
        /// <param name="student">Đối tượng Student cần tạo.</param>
        public void CreateStudent(Student student)
        {
            try
            {
                // Kiểm tra tính hợp lệ của dữ liệu
                var validation = _studentService.ValidateStudent(student);
                if (!validation.isValid)
                {
                    // Thông báo lỗi cho người dùng thông qua MessageBox (trong WinForms)
                    MessageBox.Show(validation.message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Gọi Service để thêm mới sinh viên
                _studentService.Insert(student);
                MessageBox.Show("Sinh viên được tạo thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tạo sinh viên: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tìm sinh viên theo Id.
        /// </summary>
        /// <param name="id">Id của sinh viên cần tìm.</param>
        /// <returns>Đối tượng Student nếu tìm thấy, ngược lại trả về null.</returns>
        public Student FindStudent(int id)
        {
            try
            {
                return _studentService.GetById(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tìm sinh viên: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả sinh viên.
        /// </summary>
        /// <returns>Danh sách sinh viên.</returns>
        public IEnumerable<Student> GetAllStudents()
        {
            try
            {
                return _studentService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi lấy danh sách sinh viên: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Student>();
            }
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên.
        /// </summary>
        /// <param name="student">Đối tượng Student cần cập nhật.</param>
        public void UpdateStudent(Student student)
        {
            try
            {
                var validation = _studentService.ValidateStudent(student);
                if (!validation.isValid)
                {
                    MessageBox.Show(validation.message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _studentService.Update(student);
                MessageBox.Show("Sinh viên được cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi cập nhật sinh viên: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xóa sinh viên theo Id.
        /// </summary>
        /// <param name="id">Id của sinh viên cần xóa.</param>
        public void DeleteStudent(int id)
        {
            try
            {
                // Xác nhận trước khi xóa (có thể sử dụng MessageBox để hỏi người dùng)
                var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    _studentService.Delete(id);
                    MessageBox.Show("Sinh viên được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi xóa sinh viên: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
