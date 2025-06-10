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

        static public void InputCodeName()
        {
            int reporterId;
            System.Console.WriteLine("Enter Your name code - ");
            string codeName = Console.ReadLine();
            if (!IsTherePeople(codeName))
            {
                System.Console.WriteLine("Your name code was not found\n");
                System.Console.WriteLine("Enter Your full name (Put a space only between first name and last name.). - ");
                string fullName = Console.ReadLine();
                string firstName = fullName.Split(" ")[0];
                // System.Console.WriteLine("Enter Your last name - ");
                string lastName = fullName.Split(" ")[1];
                People reporter = AddPeople(firstName, lastName, "reporter");
                codeName = reporter._codeName;
                reporterId = reporter._id;
            }
            else
            {
                reporterId = FindPeopleByCN(codeName)._id;
            }
            DalReport.DataToReport(reporterId);
            UpdateType("r", codeName, reporterId);
            UpdateNum("r", codeName, reporterId);


        }

        static public void UpdateType(string type, string codeName, int id)
        {
            try
            {
                string newType = Functions.CreateType(type, codeName);
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"UPDATE peoples SET type = @type WHERE id = @id", conn);
                cmd.Parameters.AddWithValue(@"type", newType);
                cmd.Parameters.AddWithValue(@"id", id);
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                
                System.Console.WriteLine(ex.Message);
            }
        }

        static public void UpdateNum(string type, string codeName, int id)
        {
            try
            {
                if (type == "r")
                {
                    int numMentions = FindPeopleByCN(codeName)._num_mentions + 1;
                    MySqlConnection conn = _MySql.GetConnect();
                    var cmd = new MySqlCommand($"UPDATE peoples SET num_mentions = @num_mentions WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@num_mentions", numMentions);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    int numReports = FindPeopleByCN(codeName)._num_reports + 1;
                    MySqlConnection conn = _MySql.GetConnect();
                    var cmd = new MySqlCommand($"UPDATE peoples SET num_reports = @num_reports WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@num_reports", numReports);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
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
        }
        
        static public bool IsTherePeople(string codeName)
        {
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand("SELECT codeName FROM peoples", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString("codeName") == codeName)
                    {
                        return true;
                    }
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
            return false;
        }

        static public People FindPeopleByCN(string codeName)
        {
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"SELECT * FROM peoples WHERE codeName = '{codeName}'", conn);
                var reader = cmd.ExecuteReader();
                reader.Read();
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
                return FindPeopleByCN(newCodeName);
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