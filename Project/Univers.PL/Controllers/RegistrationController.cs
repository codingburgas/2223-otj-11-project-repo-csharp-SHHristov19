using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using Univers.BLL.Services;
using Univers.DAL.Entities;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UserService _userService;
        private readonly UniversityService _universityService;
        private readonly FacultyService _facultyService;
        private readonly SpecialityService _specialityService;
        private readonly StudentService _studentService;

        public RegistrationController()
        {
            _userService = new UserService();
            _universityService = new UniversityService();
            _facultyService = new FacultyService();
            _specialityService = new SpecialityService();
            _studentService = new StudentService();
        }


        public ActionResult SignUpAs()
        {
            return View();
        }
         
        [HttpPost]
        public ActionResult ChooseRoleForSignUp(SignUpUserModel user)
        {
            if (user.RoleChoice == "Студент")
            {
                return RedirectToAction("ChooseFormOfEducationAndDegree");
            }
            else
            {
                return RedirectToAction("SignUpAsStaff");
            }
        }


        public ActionResult SignUpAsStaff()
        {
            SignUpUserModel user = new();
            return View(user);
        }

        [HttpPost]
        public ActionResult AddUser(SignUpUserModel user)
        {
            ValidationResult usernameValidationResult = _userService.ValidateUsername(user);
            ValidationResult emailValidationResult = _userService.ValidateEmail(user);
            if (usernameValidationResult != ValidationResult.Success)
            {
                ModelState.AddModelError("Username", usernameValidationResult.ErrorMessage);
            }
            if (emailValidationResult != ValidationResult.Success)
            {
                ModelState.AddModelError("Email", emailValidationResult.ErrorMessage);
            }
            if (!ModelState.IsValid)
            {
                return View("SignUpAsStaff");
            }
            if (user != null)
            {
                _userService.AddUser(user);
                return RedirectToAction("SuccessfulLogin", "Login", user);
            }
            else
            {
                return View("SignUp", user);
            }
        }

        public ActionResult ChooseFormOfEducationAndDegree()
        {
            SelectionModel model = new();
            return View(model);
        }

        [HttpPost]
        public ActionResult FormOfEducationAndDegree(SelectionModel model)
        {
            if (model.FormOfEducation != null && model.Degree != null)
            {
                return RedirectToAction("SignUpAsStudent", "Registration", new { formOfEducation = model.FormOfEducation, degree = model.Degree });
            }
            else
            {
                return RedirectToAction("ChooseFormOfEducationAndDegree");
            }
        }

        public ActionResult SignUpAsStudent(string formOfEducation, string degree)
        {
            StudentModel student = new();
            student.FormOfEducation = formOfEducation;
            student.Degree = degree;
            return View(student);
        } 

        [HttpPost]
        public ActionResult AddStudent(StudentModel student, string formOfEducation, string degree)
        {
            ValidationResult usernameValidationResult = _userService.ValidateUsername(student);
            ValidationResult emailValidationResult = _userService.ValidateEmail(student);
            if (usernameValidationResult != ValidationResult.Success)
            {
                ModelState.AddModelError("Username", usernameValidationResult.ErrorMessage);
            }
            if (emailValidationResult != ValidationResult.Success)
            {
                ModelState.AddModelError("Email", emailValidationResult.ErrorMessage);
            }
            if (!ModelState.IsValid)
            { 
                return View("SignUpAsStudent");
            }
            if (student != null)
            {
                _userService.AddUser(student);
                student.UserId = _userService.GetUserIdByUsername(student.Username);
                student.Id = Guid.NewGuid().ToString("D");
                student.FormOfEducation = formOfEducation;
                _studentService.AddStudent(student);
                return RedirectToAction("ChooseUniversity", "Registration", new { studentId = student.Id, degree});
            }
            else
            {
                return View("SignUp", student);
            }
        } 


        public ActionResult ChooseUniversity(string studentId, string degree)
        {
            SelectionModel university = new();
            university.StudentId = studentId;
            university.Universities = _universityService.TransferDataFromEntityToModel();
            university.Degree = degree;
            return View(university);
        }

        [HttpPost]
        public ActionResult UniversityChoice(string universityId, string studentId, string degree)
        {
            if (universityId == null)
            {
                return View("ChooseUniversity");
            }
            else
            {
                return RedirectToAction("ChooseFaculty", "Registration", new { universityId, studentId, degree});
            }
        }

        public ActionResult ChooseFaculty(string universityId, string studentId, string degree)
        {
            SelectionModel faculty = new();
            faculty.StudentId = studentId;
            faculty.UniversityId = universityId;
            faculty.Degree = degree;
            faculty.Faculties = _facultyService.GetFacultiesByUniversityId(universityId);
            return View(faculty);
        }

        [HttpPost]
        public ActionResult FacultyChoice(string facultyId, string universityId, string studentId, string degree)
        {
            if (facultyId == null)
            {
                return View("ChooseFaculty", universityId);
            }
            else
            {
                return RedirectToAction("ChooseSpeciality", "Registration", new { facultyId, universityId, studentId, degree });
            }
        }

        public ActionResult ChooseSpeciality(string facultyId, string universityId, string studentId, string degree)
        {
            SelectionModel speciality = new();
            speciality.StudentId = studentId;
            speciality.UniversityId = universityId;
            speciality.FacultyId = facultyId;
            speciality.Faculties = _facultyService.GetFacultiesByUniversityId(universityId);
            speciality.Specialities = _specialityService.GetSpecialitiesByFacultyId(facultyId, degree); 
            return View(speciality);
        }

        [HttpPost]
        public ActionResult SpecialityChoice(string facultyId, string universityId, string specialityId, string studentId)
        {
            if (specialityId == null)
            {
                return View("ChooseSpeciality", new { facultyId, universityId});
            }
            else
            {
                _studentService.AddSpecialityId(studentId, specialityId);
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
