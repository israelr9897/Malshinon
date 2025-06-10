namespace Malshinon.models
{
    class Proram
    {
        static void Main(string[] args)
        {
            MySqlConnect conn = new MySqlConnect();
            conn.connect();
            DalPeople people = new DalPeople(conn);
            DalReport report = new DalReport(conn);
            DalPeople.InputCodeName();
        }
    }
}