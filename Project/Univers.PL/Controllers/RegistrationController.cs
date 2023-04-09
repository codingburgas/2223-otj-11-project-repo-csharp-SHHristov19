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

        public RegistrationController()
        {
            _userService = new UserService();
            _universityService = new UniversityService();
            _facultyService = new FacultyService();
            _specialityService = new SpecialityService();
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
                return RedirectToAction("SignUpAsStudent");
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


        public ActionResult SignUpAsStudent()
        {
            StudentModel student = new();
            return View(student);
        } 

        [HttpPost]
        public ActionResult AddStudent(StudentModel student)
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
                return RedirectToAction("SuccessfulLogin", "Login", student);
            }
            else
            {
                return View("SignUp", student);
            }
        } 

        public ActionResult ChooseUniversity()
        {
            UniversityFacultySpecialitySelectionModel university = new();
            university.Universities = _universityService.TransferDataFromEntityToModel();
            return View(university);
        }

        [HttpPost]
        public ActionResult UniversityChoice(string universityId)
        {
            if (universityId == null)
            {
                return View("ChooseUniversity");
            }
            else
            {
                return RedirectToAction("ChooseFaculty", "Registration", new { universityId });
            }
        }

        public ActionResult ChooseFaculty(string universityId)
        {
            UniversityFacultySpecialitySelectionModel faculty = new();
            faculty.UniversityId = universityId;
            faculty.Faculties = _facultyService.GetFacultiesByUniversityId(universityId);
            return View(faculty);
        }

        [HttpPost]
        public ActionResult FacultyChoice(string facultyId, string universityId)
        {
            if (facultyId == null)
            {
                return View("ChooseFaculty", universityId);
            }
            else
            {
                return RedirectToAction("ChooseSpeciality", "Registration", new { facultyId, universityId });
            }
        }

        public ActionResult ChooseSpeciality(string facultyId, string universityId)
        {
            UniversityFacultySpecialitySelectionModel speciality = new();
            speciality.UniversityId = universityId;
            speciality.FacultyId = facultyId;
            speciality.Faculties = _facultyService.GetFacultiesByUniversityId(universityId);
            speciality.Specialities = _specialityService.GetSpecialitiesByFacultyId(facultyId); 
            return View(speciality);
        }

        [HttpPost]
        public ActionResult SpecialityChoice(string facultyId, string universityId, string specialityId)
        {
            if (specialityId == null)
            {
                return View("ChooseSpeciality", new { facultyId, universityId});
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
