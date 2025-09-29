 using Microsoft.AspNetCore.Mvc;
using Wsi.CyFun.Elephants.Web.ViewModels;
using Wsi.CyFun.Elephants.Web.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Wsi.CyFun.Elephants.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public LoginController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel loginViewModel)
        {
            if (string.IsNullOrWhiteSpace(loginViewModel.UserName))
            {
                return View(loginViewModel);
            }

            var user = _applicationDbContext.ApplicationUsers.FirstOrDefault(u => u.Username == loginViewModel.UserName);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.Username);
                HttpContext.Session.SetString("IsAdmin", user.IsAdmin ? "true" : "false");
                HttpContext.Session.SetString("IsAssessor", user.IsAssessor ? "true" : "false");

                if (user.IsAdmin)
                    return RedirectToAction("Index", "Admin");
                if (user.IsAssessor)
                    return RedirectToAction("Index", "Assessor");
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Ongeldige gebruikersnaam.");
            return View(loginViewModel);
        }
    }
}
