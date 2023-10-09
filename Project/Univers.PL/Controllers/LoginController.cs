﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Univers.BLL.Services;
using Univers.DAL.Entities;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService;
        private readonly StudentService _studentService;
        private readonly StaffService _staffService;

        public LoginController()
        {
            _userService = new UserService();
            _studentService = new StudentService();
            _staffService = new StaffService();
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
                var student = _studentService.GetStudentByUserId(loginUser.Id);
                if (student != null)
                {
                    return RedirectToAction("StudentHome", "Home", new { studentId = student.Id });
                } 
                else
                {
                    if (loginUser.IsConfirmed == true)
                    {
                        var role = _staffService.GetStaffByUserId(loginUser.Id).Role;
                        if (role == "Администратор")
                        {
                            return RedirectToAction("AdminHome", "Home", new { userId = loginUser.Id });
                        }
                        else if(role == "Преподавател") 
                        {
                            return RedirectToAction("TeacherHome", "Home", new { userId = loginUser.Id });
                        }
                    }
                }
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
