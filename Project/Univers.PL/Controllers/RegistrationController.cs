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
            User user = new();
            return View(user);
        }

        public ActionResult SignUpAsStudent()
        {
            Student student = new();
            return View(student);
        }

        [HttpPost]
        public ActionResult AddUser(User user)
        {
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
        public ActionResult ChooseRoleForSignUp(User user)
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
