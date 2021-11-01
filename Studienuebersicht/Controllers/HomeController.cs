using Microsoft.AspNetCore.Mvc;

namespace Studienuebersicht.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
