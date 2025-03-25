using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace App.Utils
{
    public class MySqlDb
    {
        private static MySqlDb _instance = null;
        private static readonly object _lock = new object();
        private MySqlConnection _connection;

        private string _connectionString = "server=localhost;database=winformdb;user=root;password=CaVN2004;";

        private MySqlDb()
        {
            _connection = new MySqlConnection(_connectionString);
        }

        public static MySqlDb Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MySqlDb();
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

        public async Task<MySqlConnection> GetConnectionAsync()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                await _connection.OpenAsync();
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

        public async Task CloseConnectionAsync()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                await _connection.CloseAsync();
            }
        }
    }
}
