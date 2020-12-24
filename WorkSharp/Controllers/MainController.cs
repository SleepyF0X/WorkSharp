using Microsoft.AspNetCore.Mvc;

namespace WorkSharp.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}