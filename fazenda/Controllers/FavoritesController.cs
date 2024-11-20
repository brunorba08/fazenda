using Microsoft.AspNetCore.Mvc;

namespace fazenda.Controllers
{
	public class FavoritesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
