namespace Malshinon.models
{
    class Proram
    {
        static void Main(string[] args)
        {
            MySqlConnect conn = new MySqlConnect();
            conn.connect();
        }
    }
}