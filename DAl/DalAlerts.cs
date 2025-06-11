using MySql.Data.MySqlClient;

namespace Malshinon.models
{
    class DalAlerts
    {
        static public MySqlConnect _MySql;
        public DalAlerts(MySqlConnect mySql)
        {
            _MySql = mySql;
        }

        public static void AddAlerts(int TID, DateTime CAT, string reason)
        {
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"INSERT INTO alerts(targetId,created_at,reason) VALUES(@targetId,@created_at,@reason)", conn);
                cmd.Parameters.AddWithValue("@targetId", TID);
                cmd.Parameters.AddWithValue("@created_at", CAT);
                cmd.Parameters.AddWithValue("@reason", reason);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                System.Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                _MySql.Disconnect();
            }
        }
    }
}