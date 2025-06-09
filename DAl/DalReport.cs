using MySql.Data.MySqlClient;

namespace Malshinon.models
{
    public class DalReport
    {
        static public MySqlConnect _MySql = DalPeople._MySql;
        // public DalReport(MySqlConnect mySql)
        // {
        //     _MySql = mySql;
        // }
        // static public void Main()
        // {
            
        // }
        static public void DataToReport(int malshinId)
        {
            System.Console.WriteLine("Enter your report text - ");
            string text = Console.ReadLine();
            string firstName = ListText(text)[0];
            string lastName = ListText(text)[1];
            string codeName = Functions.CreatCodeName(firstName, lastName);
            string textToReport = GetText(ListText(text));
            if (!Functions.IsTherePeople(codeName))
            {
                DalPeople.AddPeople(firstName, lastName, "target");
            }
            SendReport(codeName, textToReport, malshinId);
        }

        static void SendReport(string CN, string TX, int MID)
        {
            try
            {
                People target = DalPeople.GetPeople(CN);
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand("INSERT INTU intelReports(malshinId,targetId,reportText) VALUES(@malshinId,@targetId,@reportText)", conn);
                cmd.Parameters.AddWithValue("@malshinId", MID);
                cmd.Parameters.AddWithValue("@targetId", target._id);
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

        static string[] ListText(string text)
        {
            string[] textList = text.Split(" ");
            return textList;
        }

        static public string GetText(string[] textList)
        {
            string text = "";
            for (int i = 2; i < textList.Length; i++)
            {
                text += textList[i] + " ";
            }
            System.Console.WriteLine(text);
            return text;
        }


    }
}