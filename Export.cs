using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;

namespace Malshinon.models
{
    public class Person
    {
        public string firstNameR { get; set; }
        public string lastNameR { get; set; }
        public string firstNameT { get; set; }
        public string lastNameT { get; set; }
        public string timestamp { get; set; }
        public string text { get; set; }

    }
    public class ReaderInFile
    {
        public ReaderInFile()
        {
               MySqlConnect conn = new MySqlConnect();
            conn.connect();
            new DalPeople(conn);
            new DalReport(conn);
            new DalAlerts(conn);
        }
        public string Puth = @"/Users/admin/Desktop/csharp/MySql/Malshinon/MalshinonFile.csv";
        // public string Puth { get; set; }
        public List<Person> ReaderFile()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            };
            using var reader = new StreamReader(Puth);
            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<Person>();
            List<Person> ListPerson = new List<Person>();
            foreach (var item in records)
            {
                ListPerson.Add(item);
            }
            return ListPerson;
        }
        public void ExportThenFile(List<Person> list)
        {
            string codeNameR = "";
            string codeNameT = "";
            foreach (var person in list)
            {
                string fullNameR = $"{person.firstNameR} {person.lastNameR}";
                if (!DalPeople.IsTherePeople(fullNameR))
                {
                    codeNameR = Functions.CreatCodeName(fullNameR);
                    DalPeople.AddPeople(fullNameR, codeNameR);
                }
                string fullNameT = $"{person.firstNameT} {person.lastNameT}";
                if (!DalPeople.IsTherePeople(fullNameT))
                {
                    codeNameT = Functions.CreatCodeName(fullNameT);
                    DalPeople.AddPeople(fullNameT, codeNameT);
                }
                People reporter = DalPeople.FindPeopleByCN(codeNameR);
                People target = DalPeople.FindPeopleByCN(codeNameT);
                DalReport.SendReport(target._id, person.text, reporter._id);
                DalPeople.UpdateNumMentions(reporter._num_mentions, reporter._id);
                DalPeople.UpdateNumReport(target._num_reports, target._id);
                DalPeople.UpdateType(Functions.ReturnTypeToReporter(reporter._type, reporter._num_mentions), reporter._codeName);
                DalPeople.UpdateType(Functions.ReturnTypeToTarget(target._type), target._codeName);
                Functions.Alerts(target);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            // string filePath = @"/Users/admin/Desktop/csharp/MySql/Malshinon/MalshinonFile.csv";

            // if (!File.Exists(filePath))
            // {
            //     Console.WriteLine("⚠️ The file is not found. Make sure the path is correct.");
            //     return;
            // }

            // Person p = new Person();
            ReaderInFile read = new ReaderInFile();
            read.ExportThenFile(read.ReaderFile());
            // foreach (var person in read.ReaderFile())
            // {
            //     Console.WriteLine($"First Name: {person}, Last Nsme: {person.lastName}, Name Code: {person.nameCode}, Timestamp: {person.timestamp}");
            // }
        }
    }
}
