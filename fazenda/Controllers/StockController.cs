using Microsoft.AspNetCore.Mvc;

namespace fazenda.Controllers
{
	public class StockController : Controller
	{
		public IActionResult Stockhome()
		{
			return View();
		}
	}
}
