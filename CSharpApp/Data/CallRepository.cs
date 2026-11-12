using Newtonsoft.Json;
using CSharpApp.Models;

namespace CSharpApp.Data
{
    public class CallRepository
    {
        private readonly string _filePath;

        public CallRepository(IWebHostEnvironment webHost)
        {
            _filePath = Path.Combine(
                webHost.ContentRootPath,
                "App_Data",
                "calls.json"
            );
        }

        public List<Call> GetAll()
        {
            using var reader = new StreamReader(_filePath);

            string json = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<List<Call>>(json)
                   ?? new List<Call>();
        }
    }
}