using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Univers.BLL.Services;
using Univers.DAL.Entities;
using Univers.Models.Models;
using Univers.Utilities;

namespace Univers.PL.Controllers
{
    public class ForgottenPasswordController : Controller
    {
        private readonly UserService _userService;
        private readonly Utilities.Utilities _utilities;

        public ForgottenPasswordController()
        {
            _userService = new UserService();
            _utilities = new Utilities.Utilities();
        }

        public ActionResult EnterEmail()
        {
            UserForgottenPasswordModel user = new();
            return View(user);
        }
         
        public ActionResult EnterCode(UserModel user)
        {
            UserForgottenPasswordModel userWithForgottenPassword = new(); 
            userWithForgottenPassword.Email = user.Email;
            return View(userWithForgottenPassword);
        }

        public ActionResult EnterCodeAgain(UserForgottenPasswordModel user)
        {
            user.Code = null;
            ModelState.AddModelError("Code", "Кодът е невалиден.");
            return View("EnterCode", user);
        }

        public ActionResult EnterNewPassword(UserForgottenPasswordModel user)
        { 
            return View(user);
        }

        [HttpPost]
        public ActionResult EmailAuthentication(UserForgottenPasswordModel user)
        {
            UserModel userWithForgottenPassword = _userService.GetUserByEmail(user.Email);
            if (userWithForgottenPassword != null)
            {
                return RedirectToAction("EnterCode", userWithForgottenPassword);
            }
            else
            {
                ModelState.AddModelError("Email", "Email адреса не е намерен."); 
                return View("EnterEmail", userWithForgottenPassword);
            } 
        }

        [HttpPost]
        public ActionResult CodeAuthentication(UserForgottenPasswordModel user)
        {
            string? code = null;
            if (user.Code != null)
            {
                foreach (var item in user.Code)
                {
                    code += item.ToString();
                }
            }
            if (code == null)
            { 
                return RedirectToAction("EnterCodeAgain", user);
            }
            if(/*_utilities.GeneratePin()*/ "123456" == code)
            {
                return RedirectToAction("EnterNewPassword", user);
            } 
            else
            { 
                return RedirectToAction("EnterCodeAgain", user);
            }
        }

        [HttpPost]
        public ActionResult NewPassword(UserForgottenPasswordModel user)
        {
            //UserModel findUser = _userService.GetUserByEmial(user.Username, user.Password);
            //_userService.ChangePassword(user.NewPassword);
            if (!(ModelState.IsValid))
            {
                return View("EnterNewPassword", user); 
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
