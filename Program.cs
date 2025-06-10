namespace Malshinon.models
{
    class Proram
    {
        static void Main(string[] args)
        {
            MySqlConnect conn = new MySqlConnect();
            conn.connect();
            new DalPeople(conn);
            new DalReport(conn);
            DalPeople.InputCodeName();
            
        }
    }
}