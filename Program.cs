namespace Malshinon.models
{
    class Proram
    {
        static void Main(string[] args)
        {
            MySqlConnect conn = new MySqlConnect();
            conn.connect();
            DalPeople pepole = new DalPeople(conn);
            pepole.SendReport("kjhkjh");
        }
    }
}