using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
            UserLoginModel user = new();
            return View(user);
        }

        public ActionResult SuccessfulLogin(UserModel user)
        {
            return View(user);
        } 

        [HttpPost]
        public ActionResult Authorization(UserLoginModel user)
        {
            UserModel loginUser = _userService.GetUserByUsernameAndPassword(user.Username, user.Password);
            if (loginUser != null)
            {
                return RedirectToAction("SuccessfulLogin", loginUser);
            }
            if(!(ModelState.IsValid))
            {
                return View("Login", user);
            }
            else
            {
                ModelState.AddModelError("UsernameLogin", "Потребителското име не е намерено.");
                ModelState.AddModelError("PasswordLogin", "Паролата не е намерена.");
                return View("Login", user);
            }
        } 
    }
}
