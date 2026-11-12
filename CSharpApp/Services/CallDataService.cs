using CSharpApp.Models;

namespace CSharpApp.Services
{
    public class CallDataService
    {
        public List<string> GetAllEmployeeNames(
            List<Call> calls)
        {
            return calls
                .SelectMany(c => c.Employees)
                .Select(e => e.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToList();
        }

        public List<FlatData> GetFlatRecords(
            List<Call> calls,
            List<string> allEmployeeNames)
        {
            return calls
                .SelectMany(c =>
                    FlatCallData(c, allEmployeeNames))
                .OrderBy(f => f.Date)
                .ToList();
        }

        private IEnumerable<FlatData> FlatCallData(
            Call call,
            IEnumerable<string> allEmployeeNames)
        {
            var dailyEmpDict = call.Employees
                .ToDictionary(e => e.Name, e => e.Calls);

            return allEmployeeNames.Select(name => new FlatData
            {
                Date = call.Date,
                Name = name,
                Calls = dailyEmpDict.GetValueOrDefault(name, 0)
            });
        }
    }
}