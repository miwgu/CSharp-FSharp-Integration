using System.Runtime.CompilerServices;

namespace UtvecklareTest.Models
{
    public class Call
    {
        public DateTime Date { get; set; }
        public List<Employee> Employees { get; set; } = new();

        public IEnumerable<FlatData> FlatCallData (IEnumerable <string> allEmployeeNames)

        {    //Dictionary of employees & calls per day
            var dailyEmpDict = Employees.ToDictionary(e => e.Name, e => e.Calls);
        
        return allEmployeeNames.Select ( name => new FlatData 

        {

                Date = this.Date,
                Name = name,

            // Get the number of calls for the employee for daily.
            // If the employee is not in the day's data (false), set Calls to 0.
               Calls = dailyEmpDict.ContainsKey(name) ? dailyEmpDict[name] : 0

        });
        }
    }

    public class Employee
    {
        public string Name { get; set; } = "";
        public int Calls {  get; set; }
    }

    public class FlatData
    {
        public DateTime Date { get; set; }
        public string Name { get; set; } = "";
        public int Calls { get; set; }

        public string DateString => Date.ToString("yyyy-MM-dd");
        public string YearMonthString => Date.ToString("yyyy-MM");
        public string YearString => Date.ToString("yyyy");
    }

    public class DashboardView
    {
        //1. A flat dataset with no missing values
        public List<FlatData> FlatRecords { get; set; }= new();
        //2. Flat dataset by year -> In the future, flat data will include data spanning multiple years 
        public Dictionary<string,List<FlatData>> FlatRecordsByYear { get; set; } = new();
        //3. All employee names
        public List<string> AllEmployeeNames { get; set; } = new();
        //4. Monthly date list
        public Dictionary<string,List<string>> LabelsByMonth { get; set; } = new();
        //5. Yearly date list
        public Dictionary<string,List<string>> LabelsByYear { get; set; } = new();
        //6. Monthly Dropdown list
        public List<string> AvailableMonths { get; set; } = new();
        //7. Yearly Dropdown list
        public List<string> AvailableYears { get; set; } = new();


    }
}
