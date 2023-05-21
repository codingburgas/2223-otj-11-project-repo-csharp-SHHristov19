using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Univers.BLL.Services;
using Univers.DAL.Entities;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserService _userService;
        private readonly StudentService _studentService;
        private readonly SpecialityService _specialityService;
        private readonly UniversityService _universityService;
        private readonly FacultyService _facultyService;
        private readonly StudentCourseService _studentCourseService;
        private readonly StaffService _staffService;

        public AdminController()
        {
            _userService = new UserService();
            _studentService = new StudentService();
            _specialityService = new SpecialityService();
            _universityService = new UniversityService();
            _facultyService = new FacultyService();
            _studentCourseService = new StudentCourseService();
            _staffService = new StaffService();
        }

        public ActionResult Users(string userId, string? message = null)
        {
            var users = new AdminModel()
            {
                UserId = userId,
                Users = _userService.GetStaffUsers(), 
            };
            ViewBag.Message = message;
            return View(users);
        }

        public ActionResult Students(string userId, string? message = null)
        {
            var users = new AdminModel()
            {
                UserId = userId,
                Users = _userService.GetStudentUsers(),
            };
            ViewBag.Message = message;
            return View(users);
        }
        
        public ActionResult SeeInfoUser(string userId, string chosenUserId)
        {
            var user = new AdminModel()
            {
                UserId = userId,
                ChosenUser = _userService.GetUserByUserId(chosenUserId),
            };

            _userService.FillRolesOfTheUsers(user.ChosenUser);

            return View(user);
        }

        public ActionResult EditUser(string userId, string chosenUserId)
        {
            var user = new AdminModel()
            {
                UserId = userId,
                ChosenUser = _userService.GetUserByUserId(chosenUserId),
                EditUser = new EditUserModel(),
            };

            user.EditUser.Id = user.ChosenUser.Id;
            user.EditUser.FirstName = user.ChosenUser.FirstName;
            user.EditUser.MiddleName = user.ChosenUser.MiddleName;
            user.EditUser.LastName = user.ChosenUser.LastName;
            user.EditUser.Username = user.ChosenUser.Username;
            user.EditUser.PhoneNumber = user.ChosenUser.PhoneNumber;
            user.EditUser.Address = user.ChosenUser.Address;
            user.EditUser.Gender = user.ChosenUser.Gender;
            user.EditUser.Email = user.ChosenUser.Email;

            var staffModel =  _staffService.GetStaffByUserId(user.ChosenUser.Id);
            user.EditUser.Staff = staffModel != null ? staffModel : new StaffModel();

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(AdminModel user)
        {
            var chosenUser = _userService.GetUserByUserId(user.EditUser.Id);
            if (user.EditUser.Username != chosenUser.Username)
            {
                ValidationResult usernameValidationResult = _userService.ValidateUsername(user.EditUser.Username);
                if (usernameValidationResult != ValidationResult.Success)
                {
                    ModelState.AddModelError("EditUser.Username", usernameValidationResult.ErrorMessage);
                }
            }
            if (user.EditUser.Email != chosenUser.Email)
            {
                ValidationResult emailValidationResult = _userService.ValidateEmail(user.EditUser.Email);
                if (emailValidationResult != ValidationResult.Success)
                {
                    ModelState.AddModelError("EditUser.Email", emailValidationResult.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {
                if(user.EditUser.Staff.Id == null)
                {
                    _staffService.AddStaffByUserId(user.EditUser.Id, user.EditUser.Staff.Role);
                }
                else
                {
                    _staffService.UpdateStaffRoleByUserId(user.EditUser.Id, user.EditUser.Staff.Role);
                }
                _userService.UpdateUser(user.EditUser);
                string msg = $"Успешно редактиране на {user.EditUser.FirstName} {user.EditUser.LastName}!";
                return RedirectToAction("Users", new { userId = user.UserId, message = msg});
            }
            else
            {
                return View("EditUser", user);
            } 
        }

        [HttpPost]
        public ActionResult Delete(string userId, string chosenUserId, string chosenUserName)
        { 
            _userService.DeleteUser(chosenUserId);
            string msg = $"Успешно изтриване на {chosenUserName}!";
            return RedirectToAction("Users", new { userId, message = msg }); 
        }

        public ActionResult AddUser(string userId)
        {
            var user = new AdminModel()
            {
                UserId = userId, 
                AddUser = new AddUserModel(),
            };

            user.AddUser.Staff = new();
            return View(user);
        }

        [HttpPost]
        public ActionResult AddUser(AdminModel user)
        {
            ValidationResult usernameValidationResult = _userService.ValidateUsername(user.AddUser.Username);
            ValidationResult emailValidationResult = _userService.ValidateEmail(user.AddUser.Email);
            if (usernameValidationResult != ValidationResult.Success)
            {
                ModelState.AddModelError("AddUser.Username", usernameValidationResult.ErrorMessage); 
            }
            if (emailValidationResult != ValidationResult.Success)
            {
                ModelState.AddModelError("AddUser.Email", emailValidationResult.ErrorMessage);
            }
            if (ModelState.IsValid)
            { 
                user.AddUser.Id = Guid.NewGuid().ToString("D"); 
                _userService.AddNewUserFromAdminPanel(user.AddUser);
                _staffService.AddStaffByUserId(user.AddUser.Id, user.AddUser.Staff.Role);
                string msg = $"Успешно добавяне на {user.AddUser.FirstName} {user.AddUser.LastName}!";
                return RedirectToAction("Users", new { userId = user.UserId, message = msg });
            }
            else
            {
                return View("AddUser", user);
            }
        }

        public ActionResult SeeInfoStudent(string userId, string chosenUserId)
        {
            var student = new AdminModel()
            {
                UserId = userId,
                ChosenStudent = _studentService.GetStudentByUserId(chosenUserId),
            };

            student.ChosenStudent.User = _userService.GetUserByStudentId(student.ChosenStudent.Id);
            student.ChosenStudent.Speciality = _specialityService.GetSpecialityNameByStudentId(student.ChosenStudent.Id);  
            student.ChosenStudent.NameOfUniversity = _universityService.GetUniversityNameByStudentId(student.ChosenStudent.Id); 
            student.ChosenStudent.Degree = _specialityService.GetDegreeByStudentId(student.ChosenStudent.Id);

            return View(student);
        }

        public ActionResult EditStudent(string userId, string chosenUserId)
        {
            var student = new AdminModel()
            {
                UserId = userId,
                ChosenStudent = _studentService.GetStudentByUserId(chosenUserId),
                EditStudent = new EditStudentModel(),
            };

            var user = _userService.GetUserByStudentId(student.ChosenStudent.Id);
            student.EditStudent.User = new();
            student.EditStudent.User.Id = user.Id;
            student.EditStudent.User.FirstName = user.FirstName;
            student.EditStudent.User.MiddleName = user.MiddleName;
            student.EditStudent.User.LastName = user.LastName;
            student.EditStudent.User.Username = user.Username;
            student.EditStudent.User.PhoneNumber = user.PhoneNumber;
            student.EditStudent.User.Address = user.Address;
            student.EditStudent.User.Gender = user.Gender;
            student.EditStudent.User.Email = user.Email;
            student.EditStudent.Id = student.ChosenStudent.Id;
            student.EditStudent.Identity = student.ChosenStudent.Identity;
            student.EditStudent.AreaOfBirth = student.ChosenStudent.AreaOfBirth;
            student.EditStudent.CountryOfBirth = student.ChosenStudent.CountryOfBirth;
            student.EditStudent.CityOfBirth = student.ChosenStudent.CityOfBirth; 
            student.EditStudent.DateOfBirth = student.ChosenStudent.DateOfBirth != null ? student.ChosenStudent.DateOfBirth.Value.ToString("yyyy-MM-dd") : null;
            student.EditStudent.MunicipalityOfBirth = student.ChosenStudent.MunicipalityOfBirth;
            student.EditStudent.Citizenship = student.ChosenStudent.Citizenship;
            student.EditStudent.DateOfGraduate = student.ChosenStudent.DateOfGraduate != null ? student.ChosenStudent.DateOfGraduate.Value.ToString("yyyy-MM-dd") : null;
            student.EditStudent.DateOfStarting = student.ChosenStudent.DateOfStarting != null ? student.ChosenStudent.DateOfStarting.Value.ToString("yyyy-MM-dd") : null;
            student.EditStudent.FormOfEducation = student.ChosenStudent.FormOfEducation; 

            return View(student);
        }

        [HttpPost]
        public ActionResult EditChosenStudent(AdminModel user)
        {
            var chosenUser = _userService.GetUserByUserId(user.EditStudent.User.Id);
            if (user.EditStudent.User.Username != chosenUser.Username)
            {
                ValidationResult usernameValidationResult = _userService.ValidateUsername(user.EditStudent.User.Username); 
                if (usernameValidationResult != ValidationResult.Success)
                {
                    ModelState.AddModelError("EditStudent.User.Username", usernameValidationResult.ErrorMessage);
                } 
            }
            if (user.EditStudent.User.Email != chosenUser.Email)
            { 
                ValidationResult emailValidationResult = _userService.ValidateEmail(user.EditStudent.User.Email); 
                if (emailValidationResult != ValidationResult.Success)
                {
                    ModelState.AddModelError("EditStudent.User.Email", emailValidationResult.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {
                _studentService.UpdaateStudent(user.EditStudent);
                string msg = $"Успешно редактиране на {user.EditStudent.User.FirstName} {user.EditStudent.User.LastName}!";
                return RedirectToAction("Students", new { userId = user.UserId, message = msg });
            }
            else
            {
                return View("EditStudent", user);
            }
        }

        [HttpPost]
        public ActionResult DeleteStudent(string userId, string chosenUserId, string chosenUserName)
        {
            _userService.DeleteUser(chosenUserId);
            string msg = $"Успешно изтриване на {chosenUserName}!";
            return RedirectToAction("Students", new { userId, message = msg });
        }

        public ActionResult AddStudent(string userId)
        {
            var student = new AdminModel()
            {
                UserId = userId,
                AddStudent = new AddStudentModel(),
            };

            return View(student);
        }

        [HttpPost]
        public ActionResult AddStudent(AdminModel student)
        {
            ValidationResult usernameValidationResult = _userService.ValidateUsername(student.AddStudent.User.Username);
            ValidationResult emailValidationResult = _userService.ValidateEmail(student.AddStudent.User.Email);
            if (usernameValidationResult != ValidationResult.Success)
            {
                ModelState.AddModelError("AddStudent.User.Username", usernameValidationResult.ErrorMessage);
            }
            if (emailValidationResult != ValidationResult.Success)
            {
                ModelState.AddModelError("AddStudent.User.Email", emailValidationResult.ErrorMessage);
            }
            if (ModelState.IsValid)
            {
                TempData["Degree"] = student.AddStudent.Degree;
                TempData["Education"] = student.AddStudent.FormOfEducation;
                student.AddStudent.Id = Guid.NewGuid().ToString("D");
                TempData["StudentId"] = student.AddStudent.Id;
                _userService.AddNewUserFromAdminPanel(student.AddStudent.User);
                student.AddStudent.User.Id = _userService.GetUserIdByUsername(student.AddStudent.User.Username);
                _studentService.AddStudent(student.AddStudent);
                return RedirectToAction("ChooseUniversity", student); 
            }
            else
            {
                return View("AddStudent", student);
            }
        }

        public ActionResult ChooseUniversity(AdminModel student)
        { 
            student.Universities = _universityService.TransferDataFromEntityToModel(); 

            return View(student);
        }

        [HttpPost]
        public ActionResult AddUnuverrsityToStudent(AdminModel student)
        { 
            return RedirectToAction("ChooseFaculty", new { userId = student.UserId, universityId = student.AddStudent.University.Id });
        }

        public ActionResult ChooseFaculty(string userId, string universityId)
        {
            AdminModel student = new AdminModel
            {
                UserId = userId,
                Faculties = _facultyService.GetFacultiesByUniversityId(universityId),
            };

            student.AddStudent = new();
            student.AddStudent.University = new();
            student.AddStudent.University.Id = universityId;

            return View(student);
        }

        [HttpPost]
        public ActionResult AddFacultyToStudent(AdminModel student)
        {
            return RedirectToAction("ChooseSpeciality", new { userId = student.UserId, universityId = student.AddStudent.University.Id, facultyId = student.AddStudent.Faculty.Id });
        }

        public ActionResult ChooseSpeciality(string userId, string universityId, string facultyId)
        {
            AdminModel student = new AdminModel
            {
                UserId = userId,
                Specialities = _specialityService.GetSpecialitiesByFacultyId(facultyId, TempData["Degree"] as string),
            };

            student.AddStudent = new();
            student.AddStudent.University = new();
            student.AddStudent.Faculty = new();

            student.AddStudent.University.Id = universityId;
            student.AddStudent.Faculty.Id = facultyId;

            return View(student);
        }

        [HttpPost]
        public ActionResult AddSpecialityToStudent(AdminModel student)
        {
            _studentService.AddFacultyNumber(TempData["StudentId"].ToString(), _facultyService.GetFacultyCode(student.AddStudent.Faculty.Id), _specialityService.GetSpecialityCode(student.AddStudent.Speciality.Id));
            _studentService.AddSpecialityId(TempData["StudentId"].ToString(), student.AddStudent.Speciality.Id);
            _studentCourseService.AddStudentCourseByDefault(TempData["StudentId"].ToString(), TempData["Education"].ToString(), student.AddStudent.University.Id);

            string msg = $"Успешно добавяне на студент!";
            return RedirectToAction("Students", new { userId = student.UserId, message = msg });
        }   
    }
} 