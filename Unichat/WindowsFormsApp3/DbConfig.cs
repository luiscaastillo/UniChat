using MySql.Data.MySqlClient;

namespace Unichat
{
    public static class DbConfig
    {
        public static readonly string connectionString = "server=12.0.0.0;uid=root;pwd=rootroot;database=unichat";

        public static MySqlConnection GetOpenConnection()
        {
            try
            {
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
            }
            catch (MySqlException ex)
            {
                return null;
            }
        }
    }
}
