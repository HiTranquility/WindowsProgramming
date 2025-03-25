using App.Models;
using App.Repositories;
using App.Utils;
using App.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Services
{
    internal class AccountSvc : IGenericService<AccountRepo, Account>
    {
        private readonly AccountRepo _accountRepo;

        public AccountSvc()
        {
            // Trong thực tế nên sử dụng Dependency Injection để inject AccountRepo
            _accountRepo = new AccountRepo();
        }

        #region Synchronous Methods

        public IEnumerable<Account> GetAll()
        {
            try
            {
                return _accountRepo.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving accounts.", ex);
            }
        }

        public Account GetById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid id.");

                return _accountRepo.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving account by id.", ex);
            }
        }

        public void Insert(Account account)
        {
            try
            {
                var validation = ValidateAccount(account);
                if (!validation.isValid)
                    throw new Exception(validation.message);

                _accountRepo.Insert(account);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting account.", ex);
            }
        }

        public void Update(Account account)
        {
            try
            {
                var validation = ValidateAccount(account);
                if (!validation.isValid)
                    throw new Exception(validation.message);

                _accountRepo.Update(account);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating account.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid id.");

                _accountRepo.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting account.", ex);
            }
        }

        #endregion

        #region Asynchronous Methods

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            try
            {
                return await _accountRepo.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving accounts asynchronously.", ex);
            }
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid id.");

                return await _accountRepo.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving account by id asynchronously.", ex);
            }
        }

        public async Task InsertAsync(Account account)
        {
            try
            {
                var validation = ValidateAccount(account);
                if (!validation.isValid)
                    throw new Exception(validation.message);

                await _accountRepo.InsertAsync(account);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting account asynchronously.", ex);
            }
        }

        public async Task UpdateAsync(Account account)
        {
            try
            {
                var validation = ValidateAccount(account);
                if (!validation.isValid)
                    throw new Exception(validation.message);

                await _accountRepo.UpdateAsync(account);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating account asynchronously.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid id.");

                await _accountRepo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting account asynchronously.", ex);
            }
        }

        #endregion

        /// <summary>
        /// Hàm kiểm tra tính hợp lệ của đối tượng Account.
        /// Trả về tuple (isValid, message) với isValid = false nếu có lỗi, message chứa thông báo lỗi.
        /// </summary>
        /// <param name="account">Đối tượng Account cần kiểm tra</param>
        /// <returns>(bool isValid, string message)</returns>
        public (bool isValid, string message) ValidateAccount(Account account)
        {
            if (account == null)
                return (false, "Account cannot be null.");

            if (string.IsNullOrWhiteSpace(account.Username))
                return (false, "Username cannot be empty.");

            if (string.IsNullOrWhiteSpace(account.Password))
                return (false, "Password cannot be empty.");

            if (string.IsNullOrWhiteSpace(account.Email))
                return (false, "Email cannot be empty.");

            // Có thể bổ sung kiểm tra định dạng email hoặc các ràng buộc khác nếu cần.

            return (true, string.Empty);
        }
    }
}
