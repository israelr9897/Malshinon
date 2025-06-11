
namespace Malshinon.models
{
    public class Functions
    {
        static public string ReturnTypeToTarget(string type)
        {
            if (type == "reporter")
            {
                return "both";
            }
            else if (type == "runing")
            {
                return "target";
            }
            return type;
        }
        public static string ReturnTypeToReporter(string type, int num)
        {
            if ((type == "reporter") || (type == "both"))
            {
                if (num >= 10)
                {
                    return "potential_agent";
                }
                return type;
            }
            else if (type == "target")
            {
                return "both";
            }
            else if (type == "runing")
            {
                return "reporter";
            }
            return type;
        }
        static public string CreatCodeName(string fullName)
        {
            string FN = fullName.Split(" ")[0];
            string LN = fullName.Split(" ")[1];
            return FN[0].ToString() + FN[^1].ToString() + LN[0].ToString() + LN[^1].ToString();
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
            return report._reportText.Split(" ").Length;
        }
        public static string InputCodeName()
        {
            System.Console.WriteLine("Enter Your name code - ");
            string codeName = Console.ReadLine();
            if (!DalPeople.IsTherePeople(codeName))
            {
                System.Console.WriteLine("Your name code was not found\n");
                System.Console.WriteLine("Enter Your full name (Put a space only between first name and last name) - ");
                string fullName = ChecksFullName();
                string newCodeName = Functions.CreatCodeName(fullName);
                DalPeople.AddPeople(fullName, newCodeName);
                return newCodeName;
            }
            return codeName;

        }
        static public string InputText()
        {
            System.Console.WriteLine("Enter your report text - ");
            return Console.ReadLine();
        }
        static public string InputFullNameToTarget()
        {
            Console.WriteLine("Enter the full name of the target (Put a space only between first name and last name.) - ");
            string fullName = ChecksFullName();
            string codeName = Functions.CreatCodeName(fullName);
            if (!DalPeople.IsTherePeople(codeName))
            {
                DalPeople.AddPeople(fullName, codeName);
            }
            return fullName;
        }
        public static string ChecksFullName()
        {
            string fullName = Console.ReadLine();
            while ((fullName.Split(" ").Length < 2) || (fullName[0].ToString() == " "))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("The name you entered is invalid,\nplease enter a valid first name and last name.");
                Console.ForegroundColor = ConsoleColor.White;
                fullName = Console.ReadLine();
            }
            return fullName;
        }
        static public void Alerts(People target)
        {

            bool IsNumReportsIn15Min = DalReport.CheckNumReportsIn15Min(target._id, ReturnTimeNow(), ReturnTimeSub15Min()) >= 3;
            if (IsNumReportsIn15Min)
            {
                NumReportsIn15Min(target._id);
            }
            if (target._num_reports + 1 >= 20)
            {
                TenReportsToThisTarget(target._id);
            }
        }
        static public void NumReportsIn15Min(int id)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string reason3TimesIn15Min = "⚠️ Alert ⚠️\nFor this purpose, 3 reports were reported in 15 minutes.";
            System.Console.WriteLine(reason3TimesIn15Min);
            Console.ForegroundColor = ConsoleColor.White;
            DalAlerts.AddAlerts(id, DateTime.Parse(ReturnTimeNow()), reason3TimesIn15Min);

        }
        static public void TenReportsToThisTarget(int id)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string reasonTenTimes = "⚠️ Alert ⚠️\nThis target has been reported over 10 times.";
            System.Console.WriteLine(reasonTenTimes);
            Console.ForegroundColor = ConsoleColor.White;
            DalAlerts.AddAlerts(id, DateTime.Parse(ReturnTimeNow()), reasonTenTimes);
        }

        static public string ReturnTimeNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }
        static public string ReturnTimeSub15Min()
        {
            return DateTime.Now.AddMinutes(-15).ToString("yyyy-MM-dd HH:mm");
        }
    }
}