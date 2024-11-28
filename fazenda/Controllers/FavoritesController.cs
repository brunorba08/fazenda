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

        // A��o GET para exibir os favoritos
        public IActionResult Index()
        {
            // Aqui voc� pode pegar os produtos favoritados do banco de dados
            var favorites = _context.Favorites.Where(f => f.UserId == 1).ToList(); // Exemplo com UserId fixo

            return View(favorites);
        }

        // A��o para adicionar um produto aos favoritos
        [HttpPost]
        public IActionResult AddToFavorites(string productName)
        {
            var userId = 1; // Aqui voc� pode pegar o UserId de uma sess�o ou cookie

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
