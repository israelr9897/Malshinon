using MySql.Data.MySqlClient;

namespace Malshinon.models
{
    public class DalPeople
    {
        static public MySqlConnect _MySql;
        public DalPeople(MySqlConnect mySql)
        {
            _MySql = mySql;
        }

        static public void InputCodeName(string codeName)
        {
            int reporterId;
            if (!Functions.IsTherePeople(codeName))
            {
                System.Console.WriteLine("Your name code was not found\n");
                System.Console.WriteLine("Enter Your first name - ");
                string firstName = Console.ReadLine();
                System.Console.WriteLine("Enter Your last name - ");
                string lastName = Console.ReadLine();
                People reporter = AddPeople(firstName, lastName, "reporter");
                // reporterId = reporter._id;
                System.Console.WriteLine(reporter._id);
            }
            else
            {
                reporterId = GetPeople(codeName)._id;
            }
            System.Console.WriteLine("find");
            DalReport.DataToReport(reporterId);

        }

        static public People GetPeople(string codeName)
        {
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"SELECT * FROM peoples WHERE codeName = '{codeName}'", conn);
                var reader = cmd.ExecuteReader();
                People people = new People(
                    reader.GetString("firstName"),
                    reader.GetString("lastName"),
                    reader.GetString("codeName"),
                    reader.GetString("type"),
                    reader.GetInt32("num_reports"),
                    reader.GetInt32("num_mentions"),
                    reader.GetInt32("id")
                );
                return people;
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

        static public People AddPeople(string FN, string LN, string TY)
        {
            try
            {
                string firstName = FN;
                string lastName = LN;
                string newCodeName = Functions.CreatCodeName(firstName, lastName);
                // string type = Functions.CreatType(newCodeName);
                string type = TY;

                MySqlConnection conn = _MySql.GetConnect();

                var cmd = new MySqlCommand($"INSERT INTO peoples(firstName,lastName,codeName,type)VALUES(@firstName,@lastName,@codeName,@type)", conn);
                cmd.Parameters.AddWithValue(@"firstName", firstName);
                cmd.Parameters.AddWithValue(@"lastName", lastName);
                cmd.Parameters.AddWithValue(@"codeName", newCodeName);
                cmd.Parameters.AddWithValue(@"type", type);
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Your data has been saved in the system!");
                return GetPeople(newCodeName);
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

        

        
        

    }
}