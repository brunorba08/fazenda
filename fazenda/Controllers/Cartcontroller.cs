using Microsoft.AspNetCore.Mvc;
using fazenda.Models;
using System.Collections.Generic;

namespace fazenda.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            // Exemplo de itens no carrinho
            var cartItems = new List<CartItem>
            {
                new CartItem { Name = "Produto 1", Price = 10.99m, Quantity = 2 },
                new CartItem { Name = "Produto 2", Price = 25.50m, Quantity = 1 }
            };

            return View(cartItems);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(string productName)
        {
            TempData["Message"] = $"{productName} removido do carrinho!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            TempData["Message"] = "Carrinho limpo!";
            return RedirectToAction("Index");
        }
    }
}
