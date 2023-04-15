using Microsoft.AspNetCore.Mvc;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult ExamSession(StudentModel student)
        {
            return View(student);
        }
    }
}
