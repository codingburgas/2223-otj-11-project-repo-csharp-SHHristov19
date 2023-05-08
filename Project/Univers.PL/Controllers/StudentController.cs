using Microsoft.AspNetCore.Mvc;
using Univers.BLL.Services;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;
        private readonly ExamService _examService;
          
        public StudentController()
        { 
            _studentService = new StudentService();
            _examService = new ExamService();
        }

        public ActionResult RegularExamSession(string studentId)
        {
            StudentModel student = _studentService.GetStudentById(studentId);
            student.Exams = _examService.GetExamInfoBySemesterId(studentId);

            return View(student);
        }

        public ActionResult CorrectiveExamSession(string studentId)
        {
            StudentModel student = _studentService.GetStudentById(studentId);
            student.Exams = _examService.GetExamInfoBySemesterId(studentId);

            return View(student);
        }
    }
}