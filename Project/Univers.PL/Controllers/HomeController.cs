using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Univers.BLL.Services;

namespace Univers.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public UserService _userService;
        public UniversityService _universityService;
        public SubjectService _subjectService;
        public HolidayService _holidayService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            _userService = new UserService();
            _universityService = new UniversityService();
            _subjectService = new SubjectService();
            _holidayService = new HolidayService();
        }

        public IActionResult Index()
        {
            return View(_holidayService.TransferDataFromEntityToModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}