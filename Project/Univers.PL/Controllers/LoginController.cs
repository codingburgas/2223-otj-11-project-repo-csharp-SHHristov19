using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Univers.BLL.Services;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService;

        public LoginController()
        {
            _userService = new UserService();
        }

        // GET: Login
        public ActionResult Login()
        {
            UserModel user = new();
            return View(user);
        }

        public ActionResult SuccessfulLogin(UserModel user)
        {
            return View(user);
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
        public ActionResult Authorization(UserModel user)
        {
            UserModel loginUser = _userService.GetUserByUsernameAndPassword(user.Username, user.Password);
            if (loginUser != null)
            {
                return RedirectToAction("SuccessfulLogin", loginUser);
            }
            else
            {
                return View("Login", user);
            }
        }

        [HttpPost]
        public ActionResult AddUser(UserModel user)
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
    }
}
