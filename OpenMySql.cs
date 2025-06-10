namespace Malshinon.models
{
    public class OpenMySql
    {
        static public MySqlConnect _MySql;
        public OpenMySql(MySqlConnect mySql)
        {
            _MySql = mySql;
        }
    }
}