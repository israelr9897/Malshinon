using System.Data;
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

        static public List<Alerts> GetAllAlerts()
        {
            List<Alerts> alertsList = new List<Alerts>();
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand("SELECT * FROM alerts;", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    alertsList.Add(ReturnObjAlert(reader));
                }
            }
            catch (MySqlException ex)
            {
                System.Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                _MySql.Disconnect();
            }
            return alertsList;
        }
        static public Alerts ReturnObjAlert(MySqlDataReader reader)
        {
            Alerts alert = new Alerts(
                reader.GetInt32("targetId"),
                reader.GetDateTime("created_at"),
                reader.GetString("reason"),
                reader.GetInt32("id")
            );
            return alert;
        }
    }
}