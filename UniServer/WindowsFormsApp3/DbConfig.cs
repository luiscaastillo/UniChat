using MySql.Data.MySqlClient;

namespace Unichat
{
    public static class DbConfig
    {
        public static readonly string connectionString = "server=127.0.0.1;uid=root;pwd=rootroot;database=unichat";

        public static MySqlConnection GetOpenConnection()
        {
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
