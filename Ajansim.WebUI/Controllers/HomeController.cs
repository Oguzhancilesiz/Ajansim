using Microsoft.AspNetCore.Mvc;

namespace Ajansim.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
