using Microsoft.VisualBasic;
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
        static public void UpdatetoPotentialAgent(int id)
        {
            MySqlConnection conn = _MySql.GetConnect();
            var cmd = new MySqlCommand($"UPDATE peoples SET type = 'potential_agent' WHERE id = @id", conn);
            cmd.Parameters.AddWithValue(@"id", id);
            cmd.ExecuteNonQuery();
        }

        static public List<People> GetAllPeoples()
        {
            List<People> peopleList = new List<People>();
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand("SELECT * FROM peoples;", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    peopleList.Add(ReturnObjPeople(reader));
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
        static public List<People> GetAllPotentialAgents()
        {
            List<People> potentialAgentsList = new List<People>();
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand("SELECT * FROM peoples WHERE type = 'potential_agent';", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    potentialAgentsList.Add(ReturnObjPeople(reader));
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
            return potentialAgentsList;
        }
        static public List<People> GetAllDangersTarget()
        {
            List<People> DangersTargetList = new List<People>();
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"SELECT * FROM  peoples p INNER JOIN alerts a on a.targetId = p.id WHERE p.type = 'target' or p.type = 'both';", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DangersTargetList.Add(ReturnObjPeople(reader));
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
            return DangersTargetList;
        }
        static public void UpdateType(string type, string codeName)
        {
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"UPDATE peoples SET type = @type WHERE codeName = @codeName", conn);
                cmd.Parameters.AddWithValue(@"type", type);
                cmd.Parameters.AddWithValue(@"codeName", codeName);
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex.Message);
            }
        }

        static public void UpdateNumReport(int num, int id)
        {
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"UPDATE peoples SET num_reports = @num_reports WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@num_reports", num +1);
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
        static public void UpdateNumMentions(int num, int id)
        {
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"UPDATE peoples SET num_mentions = @num_mentions WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@num_mentions", num +1);
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

        static public People ReturnObjPeople(MySqlDataReader reader)
        {
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

        static public People FindPeopleById(int id)
        {
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"SELECT * FROM peoples WHERE id = '{id}'", conn);
                var reader = cmd.ExecuteReader();
                reader.Read();
                return ReturnObjPeople(reader);
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
        static public People FindPeopleByCN(string codeName)
        {
            try
            {
                MySqlConnection conn = _MySql.GetConnect();
                var cmd = new MySqlCommand($"SELECT * FROM peoples WHERE codeName = '{codeName}'", conn);
                var reader = cmd.ExecuteReader();
                reader.Read();
                return ReturnObjPeople(reader);
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

        static public People AddPeople(string fullName, string codeName)
        {
            try
            {
                string firstName = fullName.Split(" ")[0];
                string lastName = fullName.Split(" ")[1];
                MySqlConnection conn = _MySql.GetConnect();

                var cmd = new MySqlCommand($"INSERT INTO peoples(firstName,lastName,codeName)VALUES(@firstName,@lastName,@codeName)", conn);
                cmd.Parameters.AddWithValue(@"firstName", firstName);
                cmd.Parameters.AddWithValue(@"lastName", lastName);
                cmd.Parameters.AddWithValue(@"codeName", codeName);
                cmd.ExecuteNonQuery();
                return FindPeopleByCN(codeName);
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