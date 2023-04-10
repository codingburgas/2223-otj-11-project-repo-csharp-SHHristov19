using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Univers.BLL.Services;
using Univers.DAL.Entities;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class ForgottenPasswordController : Controller
    {
        private readonly UserService _userService;
        private readonly Univers.Utilities.Utilities _utilities;
        private readonly EmailServer.EmailSender _emailSender;

        public ForgottenPasswordController()
        {
            _userService = new UserService();
            _utilities = new Univers.Utilities.Utilities();
            _emailSender = new EmailServer.EmailSender();
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
            userWithForgottenPassword.CodeCheck = user.Code;
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
            userWithForgottenPassword.Code = _utilities.GeneratePin();
            _emailSender.SendCode(userWithForgottenPassword.Email, userWithForgottenPassword.Code, userWithForgottenPassword.FirstName, userWithForgottenPassword.LastName);

            if (userWithForgottenPassword != null)
            {
                return RedirectToAction("EnterCode", userWithForgottenPassword);
            }
            else
            {
                ModelState.AddModelError("Email", "Email адреса не е намерен."); 
                return View("EnterEmail", user);
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
            if(user.CodeCheck == code)
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
            UserModel findUser = _userService.GetUserByEmail(user.Email);
            if (!(ModelState.IsValid))
            {
                return View("EnterNewPassword", user);
            }
            else
            {
                _userService.ChangePassword(findUser.Id, user.NewPassword);
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
