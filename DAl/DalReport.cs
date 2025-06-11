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
        // static public void Main()
        // {

        // }
        static public void DataToReport(int malshinId)
        {
            System.Console.WriteLine("Enter the full name of the target (Put a space only between first name and last name.). - ");
            string fullName = Console.ReadLine();
            string firstName = fullName.Split(" ")[0];
            string lastName = fullName.Split(" ")[1];
            System.Console.WriteLine("Enter your report text - ");
            string text = Console.ReadLine();
            string codeName = Functions.CreatCodeName(firstName, lastName);
            if (!DalPeople.IsTherePeople(codeName))
            {
                DalPeople.AddPeople(firstName, lastName, "target");
            }
            People target = DalPeople.FindPeopleByCN(codeName);
            SendReport(target._id, text, malshinId);
            string timeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string timeSub15 = DateTime.Now.AddMinutes(-15).ToString("yyyy-MM-dd HH:mm");
            bool IsNumReportsIn15Min = CheckNumReportsIn15Min(timeNow, timeSub15) >= 3;
            DalPeople.UpdateType("t", codeName, target._id);
            DalPeople.UpdateNumReport(codeName, target._id);
            string reason10Times = "This target has been reported over 10 times.";
            string reason3TimesIn15Min = "For this purpose, 3 reports were reported in 15 minutes.";
            if (target._num_reports + 1 >= 20)
            {
                System.Console.WriteLine(reason10Times);
                DalAlerts.AddAlerts(target._id, DateTime.Parse(timeNow), reason10Times);
            }
            if (IsNumReportsIn15Min)
            {
                DalAlerts.AddAlerts(target._id, DateTime.Parse(timeNow), reason3TimesIn15Min);
                System.Console.WriteLine(reason3TimesIn15Min);
            }
        }

        static void SendReport(int ID, string TX, int MID)
        {
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand("INSERT INTO intelReports(malshinId,targetId,reportText) VALUES(@malshinId,@targetId,@reportText)", conn);
                cmd.Parameters.AddWithValue("@malshinId", MID);
                cmd.Parameters.AddWithValue("@targetId", ID);
                cmd.Parameters.AddWithValue("@reportText", TX);
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Report sent successfully.");
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
            foreach (var report in FindAllReports())
            {
                if (report._id == id)
                {
                    return report;
                }
            }
            return null;
        }

        static public int CheckNumReportsIn15Min(string time, string timeSub15)
        {
            int count = 0;
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"SELECT COUNT(*)c FROM `intelReports` WHERE `stemptime` BETWEEN '{timeSub15}' and '{time}';", conn);
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
        static public List<Report> FindAllReports()
                {
                    List<Report> reportsList = new List<Report>();
                    try
                    {
                        MySqlConnection conn = _MySql.GetConnect();
                        var cmd = new MySqlCommand("SELECT * FROM intelReports;", conn);
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            reportsList.Add(new Report(
                                reader.GetInt32("malshinId"),
                                reader.GetInt32("targetId"),
                                reader.GetString("reportText"),
                                reader.GetDateTime("stemptime"),
                                reader.GetInt32("id")
                            ));
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
        
    }
}