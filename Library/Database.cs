using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Library
{
    public class Database
    {
        public Database() : base()
        {
            initClass();
        }

        private void initClass()
        {
            _server = "192.168.0.162";
            _database = "geldautomaat";
            _uid = "elian";
            _password = "elian";

            setupConnection();
        }

        private MySqlConnection _connection;
        private string _server;
        private string _database;
        private string _uid;
        private string _password;

        public MySqlConnection connection { get { return _connection; } }

        private void setupConnection()
        {
            string connectionString = $"Server={_server}; Database={_database};User Id={_uid};Password={_password}";
            _connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
