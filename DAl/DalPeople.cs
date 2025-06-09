using MySql.Data.MySqlClient;

namespace Malshinon.models
{
    public class DalPeople
    {
        public MySqlConnect _MySql;
        public DalPeople(MySqlConnect mySql)
        {
            _MySql = mySql;
        }

        public void SendReport(string codeName)
        {
            if (!IsTherePeople(codeName))
            {
                AddPeople(codeName);
            }
            System.Console.WriteLine("find");

        }

        public bool IsTherePeople(string codeName)
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
        public bool IsTarget(string codeName)
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
                        if ((reader.GetString("type") == "target") || (reader.GetString("type") == "both"))
                        {
                            return true;
                        }
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

        public void AddPeople(string codeName)
        {
            try
            {
                System.Console.WriteLine("Enter Your first name - ");
                string firstName = Console.ReadLine();
                System.Console.WriteLine("Enter Your last name - ");
                string lastName = Console.ReadLine();
                string newCodeName = CreatCodeName(firstName, lastName);
                System.Console.WriteLine(firstName[^1]);
                string type = CreatType(newCodeName);

                MySqlConnection conn = _MySql.GetConnect();

                var cmd = new MySqlCommand($"INSERT INTO peoples(firstName,lastName,codeName,type)VALUES(@firstName,@lastName,@codeName,@type)", conn);
                cmd.Parameters.AddWithValue(@"firstName", firstName);
                cmd.Parameters.AddWithValue(@"lastName", lastName);
                cmd.Parameters.AddWithValue(@"codeName", newCodeName);
                cmd.Parameters.AddWithValue(@"type", type);
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

        public string CreatCodeName(string FN, string LN)
        {
            string codeName = FN[0].ToString() + FN[^1].ToString() + LN[0].ToString() + LN[^1].ToString();
            return codeName;
        }

        public string CreatType(string codeName)
        {
            if (IsTherePeople(codeName))
            {
                if (IsTarget(codeName))
                {
                    return "both";
                }
            }
            return "reporter";
        }
        

    }
}