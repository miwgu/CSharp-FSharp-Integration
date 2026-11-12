using CSharpApp.Models;

namespace CSharpApp.Services
{
    public class DashboardService
    {
        public DashboardView CreateDashboard(
            List<FlatData> flatRecords,
            List<string> allEmployeeNames)
        {
            var flatRecordsByYear = flatRecords
                .GroupBy(f => f.YearString)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderBy(f => f.Date).ToList()
                );

            var labelsByMonth = flatRecords
                .GroupBy(f => f.YearMonthString)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(f => f.DateString)
                          .Distinct()
                          .OrderBy(d => d)
                          .ToList()
                );

            var labelsByYear = flatRecords
                .GroupBy(f => f.YearString)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(f => f.DateString)
                          .Distinct()
                          .OrderBy(d => d)
                          .ToList()
                );

            return new DashboardView
            {
                FlatRecords = flatRecords,
                FlatRecordsByYear = flatRecordsByYear,
                AllEmployeeNames = allEmployeeNames,
                LabelsByMonth = labelsByMonth,
                LabelsByYear = labelsByYear,
                AvailableMonths = labelsByMonth.Keys
                    .OrderBy(m => m).ToList(),
                AvailableYears = labelsByYear.Keys
                    .OrderBy(y => y).ToList()
            };
        }
    }
}