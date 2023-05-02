using Microsoft.AspNetCore.Mvc;
using Univers.BLL.Services;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentService _studentService; 

        public HomeController()
        {
            _studentService = new StudentService();
        }

        public ActionResult StaffHome()
        {
            return View();
        }
        
        public ActionResult StudentHome(string studentId, int year = 0, int month = 0)
        {  
            StudentModel student = _studentService.GetStudentById(studentId);
            DateTime currentDate = DateTime.Now;
            if (year == 0 || month == 0)
            {
                year = currentDate.Year;
                month = currentDate.Month;
            } 

            ViewBag.Year = year;
            ViewBag.Month = month;
        
            return View(student);
        }
    }
}
