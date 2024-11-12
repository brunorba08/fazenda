using Microsoft.AspNetCore.Mvc;

namespace fazenda.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}