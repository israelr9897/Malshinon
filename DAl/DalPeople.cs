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
                People p = AddPeople(firstName, lastName, "reporter");
                codeName = p._codeName;

            }
            People reporter = FindPeopleByCN(codeName);
            DalReport.DataToReport(reporter._id);
            UpdateType("r", reporter._codeName, reporter._id);
            UpdateNumMentions(reporter._codeName, reporter._id);
            bool TenWithHundred = Functions.CheckSomeBigReport(reporter._id) >= 10;
            if (TenWithHundred)
            {
                UpdatetoPotentialAgent(reporter._id);
            }
            


        }

        static public void UpdatetoPotentialAgent(int id)
        {
            MySqlConnection conn = _MySql.GetConnect();
            var cmd = new MySqlCommand($"UPDATE peoples SET type = 'potential_agent' WHERE id = @id", conn);
            cmd.Parameters.AddWithValue(@"id", id);
            cmd.ExecuteNonQuery();
        }

        static List<People> FindAllPeoples()
        {
            List<People> peopleList = new List<People>();
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand("SELECT * FROM peoples;", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    peopleList.Add(new People(
                        reader.GetString("firstName"),
                        reader.GetString("lastName"),
                        reader.GetString("codeName"),
                        reader.GetString("type"),
                        reader.GetInt32("num_reports"),
                        reader.GetInt32("num_mentions"),
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
            return peopleList;
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

        static public int CheckNumMentions(string codeName)
        {
            return FindPeopleByCN(codeName)._num_mentions;
        }
        static public int CheckNumReports(string codeName)
        {
            return FindPeopleByCN(codeName)._num_reports;
        }

        static public void UpdateNumReport(string codeName, int id)
        {
            try
            {
                int numReports = CheckNumReports(codeName) + 1;
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"UPDATE peoples SET num_reports = @num_reports WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@num_reports", numReports);
                cmd.Parameters.AddWithValue("@id", id);
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
        static public void UpdateNumMentions(string codeName, int id)
        {
            try
            {
                int numMentions = CheckNumMentions(codeName) + 1;
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"UPDATE peoples SET num_mentions = @num_mentions WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@num_mentions", numMentions);
                cmd.Parameters.AddWithValue("@id", id);
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
        
        static public bool IsTherePeople(string codeName)
        {
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"SELECT codeName FROM peoples WHERE codeName = '{codeName}';", conn);
                var reader = cmd.ExecuteReader();
                return reader.Read();
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