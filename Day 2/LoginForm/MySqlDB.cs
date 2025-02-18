using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm
{
    using System;
    using MySql.Data.MySqlClient;

    public sealed class MySqlDB
    {
        private static MySqlDB _instance = null;
        private static readonly object _lock = new object();
        private MySqlConnection _connection;

        private string _connectionString = "server=localhost;database=;user=root;password=yourpassword;";

        private MySqlDB()
        {
            _connection = new MySqlConnection(_connectionString);
        }

        public static MySqlDB Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MySqlDB();
                    }
                    return _instance;
                }
            }
        }

        public MySqlConnection GetConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
