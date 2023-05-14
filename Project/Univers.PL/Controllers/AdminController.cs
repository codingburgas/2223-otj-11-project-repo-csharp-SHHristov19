using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Univers.BLL.Services;
using Univers.Models.Models;

namespace Univers.PL.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserService _userService;

        public AdminController()
        {
            _userService = new UserService();
        }

        public ActionResult Users(string userId)
        {
            var users = new AdminUsers() 
            { 
                UserId = userId,
                Users = _userService.GetStaffUsers(),
            };

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
    }
}
