using CsvHelper;
using CsvHelper.Configuration;
using System.Formats.Asn1;
using System.Globalization;

namespace Program
{
    public class LinearRegression
    {
        public static void Main(String[] args)
        {
            List<SalaryData> users = new();
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                //Comment = '#',
                //AllowComments = true,
                //Delimiter = ";",
            };
            using var st = new StreamReader("Salary_Data.csv");
            using (var csv = new CsvReader(st, csvConfig))
            {
                users = csv.GetRecords<SalaryData>().ToList();
            }
            float sum = 0.0f;
            foreach (var user in users)
            {
                Console.WriteLine(user.Salary + " " + user.YearsExperience);

                sum += (float)user.Salary;
            }
            //Independent Variable
            var X = users.Select(x => x.Salary).Sum();

            var XX = users.Select(x => x.Salary * x.Salary).Sum();
            //Dependend Variable
            var Y = users.Select(x=>x.YearsExperience).ToList();

            var XY = users.Select(x => x.Salary * x.YearsExperience).Sum();

            float B1 = 0.0f; 
            float B0 = 0.0f;
            // learning rate
            float m = 0.01f;


            
        }
    }

    public class SalaryData
    {
        public float YearsExperience { get; set; }
        public float Salary{get;set; }
    }
}