using MySql.Data.MySqlClient;


namespace Malshinon.models
{
    public class MySqlConnect
    {
        string connectString = "Server=localhost;Database=Malshinon;User=root;Password='';";
        MySqlConnection connection;

        public void connect()
        {
            var conn = new MySqlConnection(connectString);
            connection = conn;
            try
            {
                conn.Open();
                System.Console.WriteLine("Connected to MySql database");
                conn.Close();
            }
            catch (MySqlException ex)
            {
                System.Console.WriteLine($"Eror connecting to MySql: {ex.Message}");
            }
        }

        public MySqlConnection GetConnect()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                return connection;
            }
            catch (MySqlException ex)
            {
                System.Console.WriteLine($"Eror connecting to MySql: {ex.Message}");
            }
            return null;
        }

        public void Disconnect()
        {
            connection.Close();
        }

    }
}