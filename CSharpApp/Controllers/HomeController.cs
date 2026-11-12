using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UtvecklareTest.Models;

namespace UtvecklareTest.Controllers
{
    public class HomeController : Controller
    {
        string _root {  get; set; }
        public HomeController(IWebHostEnvironment webHost)
        {
            _root = webHost.ContentRootPath;
        }


        public IActionResult Index()
        {
            List<Call> calls = GetData();

            // Create a list of all employees' names 
            var allEmployeeNames = calls
                .SelectMany(c => c.Employees)
                .Select(e => e.Name)
                .Distinct()// remove duplicate names
                .OrderBy(n => n)
                .ToList();

            //All flat data 
            var flatRecords = calls
                 .SelectMany(c => c.FlatCallData(allEmployeeNames))
                 .OrderBy(f => f.Date)
                 .ToList();

            //flat data by year
            var flatRecordsByYear = flatRecords
                 .GroupBy(f => f.YearString)
                 .ToDictionary(
                      g => g.Key,
                      g => g
                      .OrderBy(f=>f.Date)
                      .ToList()
                          );


            //Dictionary of month & list of dates in that month
            var labelsByMonth = flatRecords
              .GroupBy(f => f.YearMonthString)
              .ToDictionary(
                g => g.Key, //2025-03...
                g => g.Select(f => f.DateString)// need to select date 2024-03-11...
                               .Distinct() // remove duplicate data
                               .OrderBy(d => d)
                               .ToList()
               );

            //Dictionary of year & list of dates in that year
            var labelsByYear = flatRecords
              .GroupBy(f => f.YearString)
              .ToDictionary(
                g => g.Key, //2025...
                g => g.Select(f => f.DateString)// need to select date 2024-03-11...
                               .Distinct() // remove duplicate data
                               .OrderBy(d => d)
                               .ToList()
               );

            // labels for Monthly dropdown list
            var availableMonths = labelsByMonth.Keys.OrderBy(m => m).ToList();

            // labels for Yearly dropdown list
            var availableYears = labelsByYear.Keys.OrderBy(y => y).ToList();

            var dv = new DashboardView
            {
                FlatRecords = flatRecords,
                FlatRecordsByYear = flatRecordsByYear,
                AllEmployeeNames = allEmployeeNames,
                LabelsByMonth = labelsByMonth,
                LabelsByYear = labelsByYear,
                AvailableMonths = availableMonths,
                AvailableYears = availableYears
            };

            return View(dv);
        }

        List<Call> GetData()
        {
            using (StreamReader r = new StreamReader($"{_root}/App_Data/calls.json"))
            {
                string json = r.ReadToEnd();
                List<Call> calls = JsonConvert.DeserializeObject<List<Call>>(json) ?? new();
                return calls;
            }
        }
    }
}
