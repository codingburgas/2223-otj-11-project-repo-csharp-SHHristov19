using Microsoft.AspNetCore.Mvc;
using Univers.BLL.Services;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;
        private StudentModel student = new StudentModel();

        public StudentController()
        {
            _studentService = new StudentService(); 
        }

        public ActionResult ExamSession(string studentId)
        {
            student = _studentService.GetStudentById(studentId);
            return View(student);
        }
    }
}