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

        public AdminController()
        {
            _userService = new UserService();
            _studentService = new StudentService();
            _specialityService = new SpecialityService();
            _universityService = new UniversityService();
        }

        public ActionResult Users(string userId, string? message = null)
        {
            var users = new AdminUsers()
            {
                UserId = userId,
                Users = _userService.GetStaffUsers(), 
            };
            ViewBag.Message = message;
            return View(users);
        }

        public ActionResult Students(string userId)
        {
            var users = new AdminUsers()
            {
                UserId = userId,
                Users = _userService.GetStudentUsers(),
            };

            return View(users);
        }
        
        public ActionResult SeeInfoUser(string userId, string chosenUserId)
        {
            var user = new AdminUsers()
            {
                UserId = userId,
                ChosenUser = _userService.GetUserByUserId(chosenUserId),
            };

            _userService.FillRolesOfTheUsers(user.ChosenUser);

            return View(user);
        }

        public ActionResult EditUser(string userId, string chosenUserId)
        {
            var user = new AdminUsers()
            {
                UserId = userId,
                ChosenUser = _userService.GetUserByUserId(chosenUserId),
                NewUser = new EditUserModel(),
            };

            user.NewUser.Id = user.ChosenUser.Id;
            user.NewUser.FirstName = user.ChosenUser.FirstName;
            user.NewUser.MiddleName = user.ChosenUser.MiddleName;
            user.NewUser.LastName = user.ChosenUser.LastName;
            user.NewUser.Username = user.ChosenUser.Username;
            user.NewUser.PhoneNumber = user.ChosenUser.PhoneNumber;
            user.NewUser.Address = user.ChosenUser.Address;
            user.NewUser.Gender = user.ChosenUser.Gender;
            user.NewUser.Email = user.ChosenUser.Email; 

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(AdminUsers user)
        {
            if (ModelState.IsValid)
            {
                _userService.UpdaateUser(user.NewUser);
                string msg = "Успешно редактиране на потребителя!";
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
            return RedirectToAction("Users", new { userId = userId, message = msg }); 
        }

        public ActionResult AddUser(string userId)
        {
            var user = new AdminUsers()
            {
                UserId = userId, 
                NewUser = new EditUserModel(),
            };

            return View(user);
        }

        [HttpPost]
        public ActionResult Add(AdminUsers user)
        {
            ValidationResult usernameValidationResult = _userService.ValidateUsername(user.NewUser.Username);
            ValidationResult emailValidationResult = _userService.ValidateEmail(user.NewUser.Email);
            if (usernameValidationResult != ValidationResult.Success)
            {
                ModelState.AddModelError("NewUser.Username", usernameValidationResult.ErrorMessage); 
            }
            if (emailValidationResult != ValidationResult.Success)
            {
                ModelState.AddModelError("NewUser.Email", emailValidationResult.ErrorMessage);
            }
            if (ModelState.IsValid)
            {
                _userService.AddNewUserFromAdminPanel(user.NewUser);
                string msg = $"Успешно добавяне на {user.NewUser.FirstName} {user.NewUser.LastName}!";
                return RedirectToAction("Users", new { userId = user.UserId, message = msg });
            }
            else
            {
                return View("AddUser", user);
            }
        }

        public ActionResult SeeInfoStudent(string userId, string chosenUserId)
        {
            var student = new AdminUsers()
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
    }
}
