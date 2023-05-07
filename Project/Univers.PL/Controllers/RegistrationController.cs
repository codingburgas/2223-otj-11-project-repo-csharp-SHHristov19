using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using Univers.BLL.Services;
using Univers.DAL.Entities;
using Univers.DAL.Repositories;
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
        private readonly IWebHostEnvironment _environment;
        private readonly StudentCourseService _studentCourseService;

        private static StudentModel StudentModel = new StudentModel();
        private static string? Education = null!;

        public RegistrationController(IWebHostEnvironment environment)
        {
            _userService = new UserService();
            _universityService = new UniversityService();
            _facultyService = new FacultyService();
            _specialityService = new SpecialityService();
            _studentService = new StudentService();
            _environment = environment;
            _studentCourseService = new StudentCourseService();
        }

        public ActionResult SignUpAs(SignUpUserModel user)
        {
            return View(user);
        }
         
        [HttpPost]
        public ActionResult ChooseRoleForSignUp(SignUpUserModel user)
        {
            if (user.RoleChoice == "Студент")
            {
                return RedirectToAction("ChooseFormOfEducationAndDegree", user);
            }
            else
            {
                return RedirectToAction("SignUpAsStaff", user);
            }
        } 

        public ActionResult SignUpAsStaff(SignUpUserModel user)
        {
            return View(user);
        }

        public ActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ImageUploading(IFormFile image)
        {
            var userImage = new SignUpUserModel();
            if (image != null && image.Length > 0)
            {
                // Generate a unique name for the file using GUID
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                // Save the file to the server
                var imagePath = Path.Combine(_environment.WebRootPath, "uploads", uniqueFileName);
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                var relativePath = $"~/uploads/{Path.GetFileName(imagePath)}";
                // Save the image path to the user's account model
                userImage.Image = relativePath;

                return RedirectToAction("SignUpAs", "Registration", userImage);
            }

            return RedirectToAction("SignUpAs", "Registration", userImage);
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
                return View("SignUpAsStaff", user);
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

        public ActionResult ChooseFormOfEducationAndDegree(string image)
        {
            SelectionModel model = new();
            model.Image = image;
            StudentModel.Image = image;
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
                student.FormOfEducation = formOfEducation; 
                student.Degree = degree;
                return View("SignUpAsStudent", student);
            }
            if (student != null)
            {
                _userService.AddUser(student);
                student.UserId = _userService.GetUserIdByUsername(student.Username);
                student.Id = Guid.NewGuid().ToString("D");
                student.FormOfEducation = formOfEducation;
                _studentService.AddStudent(student);
                StudentModel = student;
                Education = formOfEducation;
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
                return View("ChooseSpeciality", new { facultyId, universityId, specialityId, studentId });
            }
            else
            {
                _studentService.AddFacultyNumber(studentId, _facultyService.GetFacultyCode(facultyId), _specialityService.GetSpecialityCode(specialityId));
                _studentService.AddSpecialityId(studentId, specialityId);
                _studentCourseService.AddStudentCourseByDefault(studentId, Education, universityId);

                StudentModel student = _studentService.GetStudentById(studentId);
                student.Address = StudentModel.Address;
                student.Degree = StudentModel.Degree;
                student.Email = StudentModel.Email;
                student.FirstName = StudentModel.FirstName;
                student.LastName = StudentModel.LastName;
                student.MiddleName = StudentModel.MiddleName;
                student.Username = StudentModel.Username;
                student.Gender = StudentModel.Gender;
                student.PhoneNumber = StudentModel.PhoneNumber;
                
                return RedirectToAction("StudentHome", "Home", student.Id);
            }
        }
    }
}
