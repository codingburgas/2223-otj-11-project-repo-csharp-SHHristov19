using Microsoft.AspNetCore.Mvc;
using Univers.BLL.Services;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UserService _userService;

        public RegistrationController()
        {
            _userService = new UserService();
        }

        public ActionResult SignUpAs()
        {
            return View();
        }

        public ActionResult SignUpAsStaff()
        {
            UserModel user = new();
            return View(user);
        }

        public ActionResult SignUpAsStudent()
        {
            StudentModel student = new();
            return View(student);
        }

        [HttpPost]
        public ActionResult AddUser(UserModel user)
        {
            if(!ModelState.IsValid)
            {
                return View("SignUpAsStaff");
            }
            if (user != null)
            {
                _userService.AddUser(user);
                return RedirectToAction("SuccessfulLogin", user);
            }
            else
            {
                return View("SignUp", user);
            }
        }

        [HttpPost]
        public ActionResult ChooseRoleForSignUp(UserModel user)
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
    }
}
