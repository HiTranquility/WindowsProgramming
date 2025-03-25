using System;
using System.Collections.Generic;
using System.Windows.Forms;
using App.Models;
using App.Services;

namespace App.Controllers
{
    internal class AccountController
    {
        private readonly AccountSvc _accountService;

        public AccountController()
        {
            // In a real application, use Dependency Injection to inject AccountSvc.
            _accountService = new AccountSvc();
        }

        #region Account Operations

        /// <summary>
        /// Registers a new account (SignUp).
        /// </summary>
        /// <param name="account">New account object</param>
        public void SignUp(Account account)
        {
            try
            {
                var validation = _accountService.ValidateAccount(account);
                if (!validation.isValid)
                {
                    MessageBox.Show(validation.message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _accountService.Insert(account);
                MessageBox.Show("Account created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during account creation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Logs in an account using username and password.
        /// </summary>
        /// <param name="username">Account username</param>
        /// <param name="password">Account password</param>
        /// <returns>The logged in account if credentials are correct; otherwise, null.</returns>
        public Account Login(string username, string password)
        {
            try
            {
                IEnumerable<Account> accounts = _accountService.GetAll();
                foreach (var account in accounts)
                {
                    // Using case-insensitive comparison for username and a simple password check.
                    if (account.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                        account.Password.Equals(password))
                    {
                        MessageBox.Show("Login successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return account;
                    }
                }
                MessageBox.Show("Invalid username or password.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during login: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Resets the password for an account.
        /// </summary>
        /// <param name="accountId">ID of the account to reset</param>
        /// <param name="newPassword">The new password</param>
        public void ResetPassword(int accountId, string newPassword)
        {
            try
            {
                // Validate new password (basic check; you may call a dedicated validation method)
                if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 8)
                {
                    MessageBox.Show("New password must be at least 8 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Retrieve the account
                Account account = _accountService.GetById(accountId);
                if (account == null)
                {
                    MessageBox.Show("Account not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Optionally, you can add more password complexity checks here.
                account.Password = newPassword;
                _accountService.Update(account);
                MessageBox.Show("Password reset successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error resetting password: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Retrieves an account by ID.
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <returns>The account object or null if not found.</returns>
        public Account GetAccount(int accountId)
        {
            try
            {
                return _accountService.GetById(accountId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving account: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Retrieves all accounts.
        /// </summary>
        /// <returns>A list of accounts.</returns>
        public IEnumerable<Account> GetAllAccounts()
        {
            try
            {
                return _accountService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving accounts: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Account>();
            }
        }

        /// <summary>
        /// Updates account information.
        /// </summary>
        /// <param name="account">Account object with updated data</param>
        public void UpdateAccount(Account account)
        {
            try
            {
                var validation = _accountService.ValidateAccount(account);
                if (!validation.isValid)
                {
                    MessageBox.Show(validation.message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _accountService.Update(account);
                MessageBox.Show("Account updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating account: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Deletes an account by ID.
        /// </summary>
        /// <param name="accountId">ID of the account to delete</param>
        public void DeleteAccount(int accountId)
        {
            try
            {
                var confirm = MessageBox.Show("Are you sure you want to delete this account?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    _accountService.Delete(accountId);
                    MessageBox.Show("Account deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting account: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
