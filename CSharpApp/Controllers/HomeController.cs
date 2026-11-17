using Microsoft.AspNetCore.Mvc;
using CSharpApp.Data;
using CSharpApp.Services;

namespace UtvecklareTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly CallRepository _repository;
        private readonly DashboardService _dashboardService;

        public HomeController(
            CallRepository repository,
            DashboardService dashboardService)
        {
            _repository = repository;
            _dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            var calls = _repository.GetAll();

            var names = FunctionalCore.CallDataService
                .getAllEmployeeNames(calls);

            var flatRecords = FunctionalCore.CallDataService
                .getFlatRecords(calls, names);

            var dashboard = _dashboardService
                .CreateDashboard(flatRecords, names);

            return View(dashboard);
        }
    }
}
