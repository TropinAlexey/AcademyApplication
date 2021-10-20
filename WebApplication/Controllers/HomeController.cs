using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebApplication.Data.Repositories;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFacultiesRepository _facultiesRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IFacultiesRepository facultiesRepository)
        {
            _logger = logger;
            _facultiesRepository = facultiesRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Overview()
        {
            return View(_facultiesRepository.GetOverview());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
