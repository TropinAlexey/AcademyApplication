using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApplication.Data.Repositories;
using WebApplication.Models;
using WebApplication.Services.Interfaces;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDepartmentsService _departmentsService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IFacultiesRepository facultiesRepository, IDepartmentsService departmentsService)
        {
            _logger = logger;
            _departmentsService = departmentsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Overview()
        {
            return View(await _departmentsService.GetOverviewAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
