using Microsoft.AspNetCore.Mvc;
using CSharpApp.Data;
using CSharpApp.Services;

namespace UtvecklareTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly CallRepository _repository;
        private readonly CallDataService _callService;
        private readonly DashboardService _dashboardService;

        public HomeController(
            CallRepository repository,
            CallDataService callService,
            DashboardService dashboardService)
        {
            _repository = repository;
            _callService = callService;
            _dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            var calls = _repository.GetAll();

            var names = _callService
                .GetAllEmployeeNames(calls);

            var flatRecords = _callService
                .GetFlatRecords(calls, names);

            var dashboard = _dashboardService
                .CreateDashboard(flatRecords, names);

            return View(dashboard);
        }
    }
}