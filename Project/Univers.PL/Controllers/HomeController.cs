using Microsoft.AspNetCore.Mvc;

namespace Univers.PL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult StaffHome()
        {
            return View();
        }
        
        public ActionResult StudentHome()
        {
            return View();
        }
    }
}
