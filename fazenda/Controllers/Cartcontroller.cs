using Microsoft.AspNetCore.Mvc;
using fazenda.Models;
using System.Collections.Generic;

namespace fazenda.Controllers
{
    public class CartController : Controller
    {
        // M�todo para exibir o carrinho
        public IActionResult Carthome()
        {
            // Verifica se j� existem itens no carrinho na sess�o
            var cartItems = TempData["CartItems"] as List<CartItem>;
            if (cartItems == null)
            {
                cartItems = new List<CartItem>(); // Se n�o houver, cria uma lista vazia
            }

            return View(cartItems);
        }

        // M�todo para remover item do carrinho
        [HttpPost]
        public IActionResult RemoveFromCart(string productName)
        {
            var cartItems = TempData["CartItems"] as List<CartItem>;
            if (cartItems != null)
            {
                var itemToRemove = cartItems.Find(item => item.Name == productName);
                if (itemToRemove != null)
                {
                    cartItems.Remove(itemToRemove);
                    TempData["Message"] = $"{productName} removido do carrinho!";
                }
            }

            TempData["CartItems"] = cartItems; // Atualiza a lista no TempData
            return RedirectToAction("Carthome");
        }

        // M�todo para limpar o carrinho
        [HttpPost]
        public IActionResult ClearCart()
        {
            TempData["CartItems"] = new List<CartItem>(); // Limpa o carrinho
            TempData["Message"] = "Carrinho limpo!";
            return RedirectToAction("Index");
        }
    }
}
