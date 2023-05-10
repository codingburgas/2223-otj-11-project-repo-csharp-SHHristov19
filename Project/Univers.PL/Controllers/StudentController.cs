using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Univers.BLL.Services;
using Univers.DAL.Entities;
using Univers.DAL.Repositories;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;
        private readonly ExamService _examService;
        private readonly UserService _userService;
        private readonly SpecialityService _specialityService;
        private readonly UniversityService _universityService;
        private readonly FacultyService _facultyService;
        private readonly GradeRepository _gradeRepository;

        public StudentController()
        {
            _studentService = new StudentService();
            _examService = new ExamService();
            _userService = new UserService();
            _specialityService = new SpecialityService();
            _universityService = new UniversityService();
            _facultyService = new FacultyService();
            _gradeRepository = new GradeRepository();
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

        public ActionResult StudentInfo(string studentId)
        {
            StudentModel student = _studentService.GetStudentById(studentId);

            student.User = _userService.GetUserByStudentId(studentId);
            student.Speciality = _specialityService.GetSpecialityNameByStudentId(studentId);
            student.Tutor = _specialityService.GetTutorNameByStudentId(studentId);
            student.Dean = _facultyService.GetDeanNameByStudentId(studentId);
            student.NameOfUniversity = _universityService.GetUniversityNameByStudentId(studentId);
            student.AdressoOfUniversity = _universityService.GetUniversityAddressByStudentId(studentId);
            student.Rector = _universityService.GetRectorByStudentId(studentId);
            student.Degree = _specialityService.GetDegreeByStudentId(studentId);

            return View(student);
        }

        public ActionResult ChangePassword(string studentId)
        {
            ChangePasswordModel model = new();
            model.StudentId = studentId;

            return View(model);
        }

        [HttpPost]
        public ActionResult NewPassword(ChangePasswordModel model)
        {
            if (!(ModelState.IsValid))
            {
                return View("ChangePassword", model);
            }
            else
            {
                if (_userService.ComparePasswordsByUserId(model.StudentId, model.Password))
                {
                    _userService.ChangePassword(_userService.GetUserByStudentId(model.StudentId).Id, model.NewPassword);
                }
                else
                {
                    return View("ChangePassword", model);
                }
                return RedirectToAction();
            }
        }

        public ActionResult ChangeUsername(string studentId)
        {
            ChangeUsernameModel model = new();
            model.StudentId = studentId;

            return View(model);
        }

        [HttpPost]
        public ActionResult NewUsername(ChangeUsernameModel model)
        {
            if (!(ModelState.IsValid))
            {
                return View("ChangeUsername", model);
            }
            else
            {
                
                if (_userService.CompareUsernames(model.StudentId, model.Username))
                {
                    
                    ValidationResult usernameValidationResult = _userService.ValidateUsername(model.NewUsername);
                    if (usernameValidationResult != ValidationResult.Success)
                    {
                        ModelState.AddModelError("NewUsername", usernameValidationResult.ErrorMessage);
                    }
                    else
                    {
                        _userService.ChangeUsername(_userService.GetUserByStudentId(model.StudentId).Id, model.NewUsername);
                        return RedirectToAction();
                    }
                    return View("ChangeUsername", model);
                }
                else
                {
                    return View("ChangeUsername", model);
                } 
            }
        }

        public ActionResult Grades(string studentId)
        {
            var studentGrades = new StudentGradesModel();

            studentGrades.StudentId = studentId;

            studentGrades.Grades = _gradeRepository.GetGradesByStudentId(studentId);

            return View(studentGrades);
        }
    }
}