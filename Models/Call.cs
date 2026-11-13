namespace CSharpApp.Models
{
    public class Call
    {
        public DateTime Date { get; set; }
        public List<Employee> Employees { get; set; } = new();
    }
}