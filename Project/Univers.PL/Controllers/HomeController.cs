using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Univers.BLL.Services;

namespace Univers.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(UniversityService.TransferDataFromEntityToModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}