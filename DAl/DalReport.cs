using MySql.Data.MySqlClient;

namespace Malshinon.models
{
    public class DalReport
    {
        static public MySqlConnect _MySql;
        public DalReport(MySqlConnect mySql)
        {
            _MySql = mySql;
        }
        static public void SendReport(int ID, string TX, int MID)
        {
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand("INSERT INTO intelReports(malshinId,targetId,reportText) VALUES(@malshinId,@targetId,@reportText)", conn);
                cmd.Parameters.AddWithValue("@malshinId", MID);
                cmd.Parameters.AddWithValue("@targetId", ID);
                cmd.Parameters.AddWithValue("@reportText", TX);
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Blue;
                System.Console.WriteLine(" ---- Report sent successfully ----\n");
                Console.ForegroundColor = ConsoleColor.White;
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

        static public Report FindReportById(int id)
        {
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"SELECT * FROM intelReports WHERE id = {id};", conn);
                var reader = cmd.ExecuteReader();
                reader.Read();
                return ReturnObjReport(reader);
            }
            catch (MySqlException ex)
            {
                System.Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                _MySql.Disconnect();
            }
            return null;
        }
        static public List<Report> FindReportsByMalshinId(int id)
        {
            List<Report> listReport = new List<Report>();
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"SELECT * FROM intelReports WHERE malshinId = {id};", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listReport.Add(ReturnObjReport(reader));
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
            return listReport;
        }

        static public int CheckNumReportsIn15Min(int id, string time, string timeSub15)
        {
            int count = 0;
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"SELECT COUNT(*)c FROM `intelReports` WHERE targetId = {id} AND `stemptime` BETWEEN '{timeSub15}' AND '{time}';", conn);
                var reader = cmd.ExecuteReader();
                reader.Read();
                count = reader.GetInt32("c");
            }
            catch (MySqlException ex)
            {
                System.Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                _MySql.Disconnect();
            }
            return count;
        }
        static public List<Report> GetAllReports()
        {
            List<Report> reportsList = new List<Report>();
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand("SELECT * FROM intelReports;", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reportsList.Add(ReturnObjReport(reader));
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
            return reportsList;
        }
        static public Report ReturnObjReport(MySqlDataReader reader)
        {
            Report report = new Report(
                reader.GetInt32("malshinId"),
                reader.GetInt32("targetId"),
                reader.GetString("reportText"),
                reader.GetDateTime("stemptime"),
                reader.GetInt32("id")
            );
            return report;
        }
    }
}