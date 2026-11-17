using Microsoft.AspNetCore.Mvc;
using CSharpApp.Data;

namespace UtvecklareTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly CallRepository _repository;

        public HomeController(CallRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var calls = _repository.GetAll();

            var names = FunctionalCore.CallDataService
                .getAllEmployeeNames(calls);

            var flatRecords = FunctionalCore.CallDataService
                .getFlatRecords(calls, names);

            var dashboard = FunctionalCore.DashboardFunctions
                .createDashboard(flatRecords, names);

            return View(dashboard);
        }
    }
}
