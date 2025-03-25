using App.Models;
using App.Utils;
using App.Utils.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Repositories
{
    public class StudentRepo : IGenericRepository<Student>
    {
        #region Synchronous Methods

        public IEnumerable<Student> GetAll()
        {
            List<Student> students = new List<Student>();
            string query = "SELECT Id, StudentId, FirstName, LastName, BirthDate, Gender, PhoneNumber, Address, Picture FROM Students";

            try
            {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            var table = new System.Data.DataTable();
                            adapter.Fill(table);

                            foreach (System.Data.DataRow row in table.Rows)
                            {
                                students.Add(MapStudent(row));
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving students.", ex);
            }
            return students;
        }

        public Student GetById(int id)
        {
            string query = "SELECT Id, StudentId, FirstName, LastName, BirthDate, Gender, PhoneNumber, Address, Picture FROM Students WHERE Id = @Id";

            try
            {
                using (MySqlConnection conn = MySqlDb.Instance.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            var table = new System.Data.DataTable();
                            adapter.Fill(table);
                            if (table.Rows.Count > 0)
                            {
                                return MapStudent(table.Rows[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving student ID {id}.", ex);
            }
            return null;
        }


        public void Insert(Student entity)
        {
            string query = @"INSERT INTO Students 
                             (StudentId, FirstName, LastName, BirthDate, Gender, PhoneNumber, Address, Picture) 
                             VALUES (@StudentId, @FirstName, @LastName, @BirthDate, @Gender, @PhoneNumber, @Address, @Picture);
                             SELECT LAST_INSERT_ID();";

            try
            {
                using (MySqlConnection conn = MySqlDb.Instance.GetConnection())
                {
                    conn.Open();
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                            {
                                BindStudentParameters(cmd, entity);
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
                throw new Exception("Error inserting student.", ex);
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Students WHERE Id = @Id";

            try
            {
                using (MySqlConnection conn = MySqlDb.Instance.GetConnection())
                {
                    conn.Open();
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
                throw new Exception($"Error deleting student ID {id}.", ex);
            }
        }

        public void Update(Student entity)
        {
            string query = @"UPDATE Students SET 
                             StudentId = @StudentId, 
                             FirstName = @FirstName, 
                             LastName = @LastName, 
                             BirthDate = @BirthDate, 
                             Gender = @Gender, 
                             PhoneNumber = @PhoneNumber, 
                             Address = @Address, 
                             Picture = @Picture 
                             WHERE Id = @Id";

            try
            {
                using (MySqlConnection conn = MySqlDb.Instance.GetConnection())
                {
                    conn.Open();
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                            {
                                BindStudentParameters(cmd, entity);
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
                throw new Exception($"Error updating student ID {entity.Id}.", ex);
            }
        }

        #endregion

        #region Asynchronous Methods

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            List<Student> students = new List<Student>();
            string query = "SELECT Id, StudentId, FirstName, LastName, BirthDate, Gender, PhoneNumber, Address, Picture FROM Students";

            try
            {
                // Use GetConnectionAsync() instead of GetConnection()
                using (MySqlConnection conn = await MySqlDb.Instance.GetConnectionAsync())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            var table = new System.Data.DataTable();
                            adapter.Fill(table);

                            foreach (System.Data.DataRow row in table.Rows)
                            {
                                students.Add(MapStudent(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving students asynchronously.", ex);
            }
            return students;
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM Students WHERE Id = @Id";

            try
            {
                using (MySqlConnection conn = await MySqlDb.Instance.GetConnectionAsync())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        object result = await cmd.ExecuteScalarAsync();
                        if (result != null)
                        {
                            using (var adapter = new MySqlDataAdapter(cmd))
                            {
                                var table = new System.Data.DataTable();
                                adapter.Fill(table);
                                if (table.Rows.Count > 0)
                                {
                                    return MapStudent(table.Rows[0]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving student ID {id} asynchronously.", ex);
            }
            return null;
        }

        public async Task InsertAsync(Student entity)
        {
            string query = @"INSERT INTO Students 
                             (StudentId, FirstName, LastName, BirthDate, Gender, PhoneNumber, Address, Picture) 
                             VALUES (@StudentId, @FirstName, @LastName, @BirthDate, @Gender, @PhoneNumber, @Address, @Picture);
                             SELECT LAST_INSERT_ID();";

            try
            {
                using (MySqlConnection conn = await MySqlDb.Instance.GetConnectionAsync())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        BindStudentParameters(cmd, entity);
                        entity.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting student asynchronously.", ex);
            }
        }

        public async Task UpdateAsync(Student entity)
        {
            string query = @"UPDATE Students SET 
                             StudentId = @StudentId, 
                             FirstName = @FirstName, 
                             LastName = @LastName, 
                             BirthDate = @BirthDate, 
                             Gender = @Gender, 
                             PhoneNumber = @PhoneNumber, 
                             Address = @Address, 
                             Picture = @Picture 
                             WHERE Id = @Id";

            try
            {
                using (MySqlConnection conn = await MySqlDb.Instance.GetConnectionAsync())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        BindStudentParameters(cmd, entity);
                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating student ID {entity.Id} asynchronously.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "DELETE FROM Students WHERE Id = @Id";

            try
            {
                using (MySqlConnection conn = await MySqlDb.Instance.GetConnectionAsync())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting student ID {id} asynchronously.", ex);
            }
        }

        #endregion

        private void BindStudentParameters(MySqlCommand cmd, Student entity)
        {
            cmd.Parameters.AddWithValue("@StudentId", entity.StudentId);
            cmd.Parameters.AddWithValue("@FirstName", entity.FirstName);
            cmd.Parameters.AddWithValue("@LastName", entity.LastName);
            cmd.Parameters.AddWithValue("@BirthDate", entity.BirthDate);
            cmd.Parameters.AddWithValue("@Gender", entity.Gender);
            cmd.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
            cmd.Parameters.AddWithValue("@Address", entity.Address);
            cmd.Parameters.AddWithValue("@Picture", entity.Picture ?? (object)DBNull.Value);
        }

        private Student MapStudent(System.Data.DataRow row)
        {
            return new Student
            {
                Id = Convert.ToInt32(row["Id"]),
                StudentId = row["StudentId"].ToString(),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                BirthDate = Convert.ToDateTime(row["BirthDate"]),
                Gender = row["Gender"].ToString(),
                PhoneNumber = row["PhoneNumber"].ToString(),
                Address = row["Address"].ToString(),
                Picture = row["Picture"] != DBNull.Value ? (byte[])row["Picture"] : null
            };
        }
    }
}
