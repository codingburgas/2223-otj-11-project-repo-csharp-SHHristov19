using Microsoft.AspNetCore.Mvc;
using Univers.BLL.Services;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;
        private readonly ExamService _examService;

        private StudentModel student = new StudentModel();

        public StudentController()
        {
            _studentService = new StudentService();
            _examService = new ExamService();
        }

        public ActionResult ExamSession(string studentId)
        {
            student = _studentService.GetStudentById(studentId);
            student.Exams = _examService.GetExamInfoBySemesterId(studentId);

            return View(student);
        }
    }
}