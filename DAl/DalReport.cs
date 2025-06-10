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
            System.Console.WriteLine("Enter the last name of the target. - ");
            string lastName = fullName.Split(" ")[1];
            System.Console.WriteLine("Enter your report text - ");
            string text = Console.ReadLine();
            string codeName = Functions.CreatCodeName(firstName, lastName);
            if (!DalPeople.IsTherePeople(codeName))
            {
                DalPeople.AddPeople(firstName, lastName, "target");
            }
            int targetId = DalPeople.FindPeopleByCN(codeName)._id;
            SendReport(targetId, text, malshinId);
            DalPeople.UpdateType("t", codeName, targetId);
            DalPeople.UpdateNum("t", codeName, targetId);
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
    }
}