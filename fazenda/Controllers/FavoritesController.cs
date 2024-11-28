using System; // Para DateTime
using System.Linq; // Para consultas LINQ
using Microsoft.AspNetCore.Mvc;
using fazenda.Data;
using fazenda.Models;


namespace fazenda.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoritesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Ação GET para exibir os favoritos
        public IActionResult Index()
        {
            // Aqui você pode pegar os produtos favoritados do banco de dados
            var favorites = _context.Favorites.Where(f => f.UserId == 1).ToList(); // Exemplo com UserId fixo

            return View(favorites);
        }

        // Ação para adicionar um produto aos favoritos
        [HttpPost]
        public IActionResult AddToFavorites(string productName)
        {
            var userId = 1; // Aqui você pode pegar o UserId de uma sessão ou cookie

            var favorite = new Favorite
            {
                UserId = userId,
                ProductName = productName,
                DateAdded = DateTime.Now
            };

            _context.Favorites.Add(favorite);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
