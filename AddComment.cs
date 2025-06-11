// namespace Malshinon.models

// {
//     public class AddComment
//     {
//         public static void StartAddComment()
//         {
//             string codeName = Functions.InputCodeName();
//             string targetFullName = Functions.InputFullNameToTarget();
//             string textToReport = Functions.InputText();
//             People reporter = DalPeople.FindPeopleByCN(codeName);
//             People target = DalPeople.FindPeopleByCN(Functions.CreatCodeName(targetFullName));
//             DalReport.SendReport(target._id, textToReport, reporter._id);
//             DalPeople.UpdateNumMentions(reporter._num_mentions, reporter._id);
//             DalPeople.UpdateNumReport(target._num_reports, target._id);
//             DalPeople.UpdateType(Functions.ReturnTypeToReporter(reporter._type, reporter._num_mentions), reporter._codeName);
//             DalPeople.UpdateType(Functions.ReturnTypeToTarget(target._type), target._codeName);
//             Functions.Alerts(target);
//         }
//     }
// }