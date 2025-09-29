using Microsoft.AspNetCore.Mvc;

namespace Wsi.CyFun.Elephants.Web.Controllers
{
    public class FunctionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
