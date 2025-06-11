
namespace Malshinon.models

{
    public class Menu
    {
        public static void MenuStart()
        {
            MySqlConnect conn = new MySqlConnect();
            conn.connect();
            new DalPeople(conn);
            new DalReport(conn);
            new DalAlerts(conn);

            ChoiceUser();
        }

        public static void ChoiceUser()
        {
            bool isExit = false;
            while (!isExit)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("\n------  Welcome  ------\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.WriteLine("Please choose from the following options - ");
                System.Console.WriteLine("1. Add a reporter");
                System.Console.WriteLine("2. See all potential reporters for agents");
                System.Console.WriteLine("3. See all dangers target");
                System.Console.WriteLine("4. See List all active alerts");
                System.Console.WriteLine("0. Exit");
                Console.ForegroundColor = ConsoleColor.White;
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddComment();
                        break;

                    case "2":
                        ShowPotentialAgent();
                        break;

                    case "3":
                        Functions.ShowDangersTarget();
                        break;

                    case "4":
                        Functions.ShowAllAlerts();
                        break;

                    case "0":
                        isExit = true;
                        break;

                }
            }


        }

        static void AddComment()
        {
            string codeName = Functions.InputCodeName();
            string targetFullName = Functions.InputFullNameToTarget();
            string textToReport = Functions.InputText();
            People reporter = DalPeople.FindPeopleByCN(codeName);
            People target = DalPeople.FindPeopleByCN(Functions.CreatCodeName(targetFullName));
            DalReport.SendReport(target._id, textToReport, reporter._id);
            DalPeople.UpdateNumMentions(reporter._num_mentions, reporter._id);
            DalPeople.UpdateNumReport(target._num_reports, target._id);
            DalPeople.UpdateType(Functions.ReturnTypeToReporter(reporter._type, reporter._num_mentions), reporter._codeName);
            DalPeople.UpdateType(Functions.ReturnTypeToTarget(target._type), target._codeName);
            Functions.Alerts(target);
        }

        static void ShowPotentialAgent()
        {
            Functions.ShowpotentialAgent();
        }
    }
}