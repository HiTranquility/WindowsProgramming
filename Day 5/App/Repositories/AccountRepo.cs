using App.Models;
using App.Utils;
using App.Utils.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Repositories
{
    public class AccountRepo : IGenericRepository<Account>
    {
        #region Synchronous Methods

        public IEnumerable<Account> GetAll()
        {
            List<Account> accounts = new List<Account>();
            string query = "SELECT Id, Username, Password, Email FROM Accounts";

            try
            {
                using (MySqlConnection conn = MySqlDb.Instance.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accounts.Add(new Account
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                Email = reader["Email"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving accounts.", ex);
            }
            return accounts;
        }

        public Account GetById(int id)
        {
            string query = "SELECT Id, Username, Password, Email FROM Accounts WHERE Id = @Id";

            try
            {
                using (MySqlConnection conn = MySqlDb.Instance.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Account
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Username = reader["Username"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    Email = reader["Email"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving account ID {id}.", ex);
            }
            return null;
        }

        public void Insert(Account entity)
        {
            string query = "INSERT INTO Accounts (Username, Password, Email) VALUES (@Username, @Password, @Email); SELECT LAST_INSERT_ID();";

            try
            {
                using (MySqlConnection conn = MySqlDb.Instance.GetConnection())
                {
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                            {
                                BindAccountParameters(cmd, entity);
                                entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting account.", ex);
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Accounts WHERE Id = @Id";

            try
            {
                using (MySqlConnection conn = MySqlDb.Instance.GetConnection())
                {
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Id", id);
                                cmd.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting account ID {id}.", ex);
            }
        }

        public void Update(Account entity)
        {
            string query = "UPDATE Accounts SET Username = @Username, Password = @Password, Email = @Email WHERE Id = @Id";

            try
            {
                using (MySqlConnection conn = MySqlDb.Instance.GetConnection())
                {
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                            {
                                BindAccountParameters(cmd, entity);
                                cmd.Parameters.AddWithValue("@Id", entity.Id);
                                cmd.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating account ID {entity.Id}.", ex);
            }
        }

        #endregion

        #region Asynchronous Methods

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            List<Account> accounts = new List<Account>();
            string query = "SELECT Id, Username, Password, Email FROM Accounts";

            try
            {
                using (MySqlConnection conn = await MySqlDb.Instance.GetConnectionAsync())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            accounts.Add(new Account
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                Email = reader["Email"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving accounts asynchronously.", ex);
            }
            return accounts;
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            string query = "SELECT Id, Username, Password, Email FROM Accounts WHERE Id = @Id";

            try
            {
                using (MySqlConnection conn = await MySqlDb.Instance.GetConnectionAsync())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Account
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Username = reader["Username"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    Email = reader["Email"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving account ID {id} asynchronously.", ex);
            }
            return null;
        }

        public async Task InsertAsync(Account entity)
        {
            string query = "INSERT INTO Accounts (Username, Password, Email) VALUES (@Username, @Password, @Email); SELECT LAST_INSERT_ID();";

            try
            {
                using (MySqlConnection conn = await MySqlDb.Instance.GetConnectionAsync())
                {
                    using (MySqlTransaction transaction = await conn.BeginTransactionAsync())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                            {
                                BindAccountParameters(cmd, entity);
                                entity.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                            }
                            await transaction.CommitAsync();
                        }
                        catch (Exception)
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting account asynchronously.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "DELETE FROM Accounts WHERE Id = @Id";

            try
            {
                using (MySqlConnection conn = await MySqlDb.Instance.GetConnectionAsync())
                {
                    using (MySqlTransaction transaction = await conn.BeginTransactionAsync())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Id", id);
                                await cmd.ExecuteNonQueryAsync();
                            }
                            await transaction.CommitAsync();
                        }
                        catch (Exception)
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting account ID {id} asynchronously.", ex);
            }
        }
        public async Task UpdateAsync(Account entity)
        {
            string query = "UPDATE Accounts SET Username = @Username, Password = @Password, Email = @Email WHERE Id = @Id";

            try
            {
                using (MySqlConnection conn = await MySqlDb.Instance.GetConnectionAsync())
                {
                    using (MySqlTransaction transaction = await conn.BeginTransactionAsync())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                            {
                                BindAccountParameters(cmd, entity);
                                cmd.Parameters.AddWithValue("@Id", entity.Id);
                                await cmd.ExecuteNonQueryAsync();
                            }
                            await transaction.CommitAsync();
                        }
                        catch (Exception)
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating account ID {entity.Id} asynchronously.", ex);
            }
        }

        #endregion

        private void BindAccountParameters(MySqlCommand cmd, Account entity)
        {
            cmd.Parameters.AddWithValue("@Username", entity.Username);
            cmd.Parameters.AddWithValue("@Password", entity.Password);
            cmd.Parameters.AddWithValue("@Email", entity.Email);
        }
    }
}
