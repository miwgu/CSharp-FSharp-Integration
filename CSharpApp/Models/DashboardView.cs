namespace CSharpApp.Models
{
    public class DashboardView
    {
        public List<FlatData> FlatRecords { get; set; } = new();

        public Dictionary<string, List<FlatData>>
            FlatRecordsByYear
        { get; set; } = new();

        public List<string> AllEmployeeNames { get; set; } = new();

        public Dictionary<string, List<string>>
            LabelsByMonth
        { get; set; } = new();

        public Dictionary<string, List<string>>
            LabelsByYear
        { get; set; } = new();

        public List<string> AvailableMonths { get; set; } = new();

        public List<string> AvailableYears { get; set; } = new();
    }
}