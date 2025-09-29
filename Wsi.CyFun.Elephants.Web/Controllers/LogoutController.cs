using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wsi.CyFun.Elephants.Web.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Remove("IsAdmin");
            HttpContext.Session.Remove("IsAssessor");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "Login");
        }

    }
}