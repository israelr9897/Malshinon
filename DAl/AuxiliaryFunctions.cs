using MySql.Data.MySqlClient;

namespace Malshinon.models
{
    public class Functions
    {
        static public string CreateType(string type, string codeName)
        {
            string peopleType = DalPeople.FindPeopleByCN(codeName)._type;
            if (type == "r")
            {
                if (peopleType == "target")
                {
                    return "both";
                }
            }
            else
                if (peopleType == "reporter")
            {
                return "both";
            }
            return peopleType;
        }
        static public string CreatCodeName(string FN, string LN)
        {
            string codeName = FN[0].ToString() + FN[^1].ToString() + LN[0].ToString() + LN[^1].ToString();
            return codeName;
        }


        static public int CheckSomeBigReport(int id)
        {
            int numBigReport = 0;
            foreach (var report in DalReport.FindAllReports())
            {
                if (report._malshinId == id && CheckLengthReport(report) >= 100)
                {
                    numBigReport += 1;
                }
            }
            return numBigReport;
        }
        static public int CheckLengthReport(Report report)
        {
            return report._reportText.Length;
        }
        // static public bool IsTarget(string codeName)
        // {
        //     try
        //     {
        //         MySqlConnection conn = _MySql.GetConnect();
        //         var cmd = new MySqlCommand("SELECT codeName FROM peoples", conn);
        //         var reader = cmd.ExecuteReader();
        //         while (reader.Read())
        //         {
        //             if (reader.GetString("codeName") == codeName)
        //             {
        //                 if ((reader.GetString("type") == "target") || (reader.GetString("type") == "both"))
        //                 {
        //                     return true;
        //                 }
        //             }
        //         }
        //     }
        //     catch (MySqlException ex)
        //     {
        //         System.Console.WriteLine($"Error: {ex.Message}");
        //     }
        //     finally
        //     {
        //         _MySql.Disconnect();
        //     }
        //     return false;
        // }
    }
}