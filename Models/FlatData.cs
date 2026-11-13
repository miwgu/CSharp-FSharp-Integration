namespace CSharpApp.Models
{
    public class FlatData
    {
        public DateTime Date { get; set; }
        public string Name { get; set; } = "";
        public int Calls { get; set; }

        public string DateString => Date.ToString("yyyy-MM-dd");
        public string YearMonthString => Date.ToString("yyyy-MM");
        public string YearString => Date.ToString("yyyy");
    }
}