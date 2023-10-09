using Microsoft.AspNetCore.Mvc;
using Univers.BLL.Services;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentService _studentService; 
        private readonly StudentCourseService _studentCourseService;
        private readonly UserService _userService;

        public HomeController()
        {
            _studentService = new StudentService();
            _studentCourseService = new StudentCourseService();
            _userService = new UserService();
        }

        public ActionResult AdminHome(string userId, string? message = null)
        {
            var users = new AdminModel()
            {
                UserId = userId,
                Users = _userService.GetUncomfirmedUsers(),
            };

            ViewBag.Message = message; 
            return View(users);
        }

        public ActionResult TeacherHome(string userId)
        {
            var user = _userService.GetUserByUserId(userId);

            return View(user);
        }
        
        public ActionResult StudentHome(string studentId, int year = 0, int month = 0)
        {  
            StudentModel? student = _studentService.GetStudentById(studentId);
            student.NameOfUniversity = _studentService.GetUniversityNameByStudentId(studentId);
            student.StudentCourse = _studentCourseService.GetStudentCourseByStudentId(studentId);
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
