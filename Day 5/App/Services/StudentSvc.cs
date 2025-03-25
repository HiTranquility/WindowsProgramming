using App.Models;
using App.Repositories;
using App.Utils.Dtos;
using App.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class StudentSvc : IGenericService<StudentRepo, StudentDto>
    {
        private readonly StudentRepo _studentRepo;

        public StudentSvc()
        {
            // Trong thực tế nên sử dụng Dependency Injection
            _studentRepo = new StudentRepo();
        }

        #region Synchronous Methods

        public IEnumerable<Student> GetAll()
        {
            try
            {
                return _studentRepo.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving students.", ex);
            }
        }

        public Student GetById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Id must be greater than zero.");

                return _studentRepo.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving student by id.", ex);
            }
        }

        public void Insert(Student entity)
        {
            try
            {
                ValidateStudent(entity);
                _studentRepo.Insert(entity); // Không cần transaction ở BLL
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting student.", ex);
            }
        }

        public void Update(Student entity)
        {
            try
            {
                ValidateStudent(entity);
                _studentRepo.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating student.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid id.");

                _studentRepo.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting student.", ex);
            }
        }

        #endregion

        #region Asynchronous Methods

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            try
            {
                var students = await _studentRepo.GetAllAsync();
                var studentDtos = students.Select(studentDto => new StudentDto
                {
                    Id = studentDto.Id,
                    StudentId = studentDto.StudentId,
                    Address = studentDto.Address,
                    PhoneNumber = studentDto.PhoneNumber,
                    BirthDate = studentDto.BirthDate,
                    FirstName = studentDto.FirstName,
                    LastName = studentDto.LastName,
                    Gender = studentDto.Gender,
                    Picture = studentDto.Picture,
                });
                return studentDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving students asynchronously.", ex);
            }
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Id must be greater than zero.");

                // Lấy Model từ repository
                var student = await _studentRepo.GetByIdAsync(id);

                var studentDto = new StudentDto()
                {
                    Id = student.Id,
                    StudentId = student.StudentId,
                    Address = student.Address,
                    PhoneNumber = student.PhoneNumber,
                    BirthDate = student.BirthDate,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Gender = student.Gender,
                    Picture = student.Picture,
                };

                // Kiểm tra null (không tìm thấy student)
                if (student == null)
                    return null; // hoặc throw Exception tùy logic của bạn

                return studentDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving student by id asynchronously.", ex);
            }
        }

        public async Task InsertAsync(StudentDto studentDto)
        {
            try
            {
                var entity = new Student()
                {
                    Id = studentDto.Id,
                    StudentId = studentDto.StudentId,
                    Address = studentDto.Address,
                    PhoneNumber = studentDto.PhoneNumber,
                    BirthDate = studentDto.BirthDate,
                    FirstName = studentDto.FirstName,
                    LastName = studentDto.LastName,
                    Gender = studentDto.Gender,
                    Picture = studentDto.Picture,
                };
                ValidateStudent(entity);
                await _studentRepo.InsertAsync(entity); // Không cần transaction ở BLL
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting student asynchronously.", ex);
            }
        }

        public async Task UpdateAsync(Student entity)
        {
            try
            {
                ValidateStudent(entity);
                await _studentRepo.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating student asynchronously.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid id.");

                await _studentRepo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting student asynchronously.", ex);
            }
        }

        #endregion

        /// <summary>
        /// Kiểm tra dữ liệu của student theo các quy tắc nghiệp vụ
        /// </summary>
        /// <param name="student">Đối tượng Student cần kiểm tra</param>
        public (bool isValid, string message) ValidateStudent(Student student)
        {
            if (student == null)
                return (false, "Đối tượng Student không được null.");

            if (string.IsNullOrWhiteSpace(student.StudentId))
                return (false, "Mã sinh viên không được để trống!");

            if (string.IsNullOrWhiteSpace(student.FirstName))
                return (false, "Tên không được để trống!");

            if (string.IsNullOrWhiteSpace(student.LastName))
                return (false, "Họ không được để trống!");

            if (student.BirthDate == default(DateTime))
                return (false, "Ngày sinh không hợp lệ!");

            if (string.IsNullOrWhiteSpace(student.Gender))
                return (false, "Giới tính không được để trống!");

            if (string.IsNullOrWhiteSpace(student.PhoneNumber))
                return (false, "Số điện thoại không được để trống!");

            return (true, string.Empty);
        }

        IEnumerable<StudentDto> IGenericService<StudentRepo, StudentDto>.GetAll()
        {
            throw new NotImplementedException();
        }

        StudentDto IGenericService<StudentRepo, StudentDto>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(StudentDto entity)
        {
            throw new NotImplementedException();
        }

        public void Update(StudentDto entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(StudentDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
