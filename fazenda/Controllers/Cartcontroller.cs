using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

public class CartController : Controller
{
    // Exibe o carrinho
    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetString("cart");
        var cartItems = string.IsNullOrEmpty(cart) ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(cart);
        return View(cartItems);
    }

    // Adiciona um item ao carrinho
    [HttpPost]
    public IActionResult AddToCart(string productName, decimal price)
    {
        var cart = HttpContext.Session.GetString("cart");
        var cartItems = string.IsNullOrEmpty(cart) ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(cart);

        var existingItem = cartItems.FirstOrDefault(x => x.Name == productName);
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            cartItems.Add(new CartItem { Name = productName, Price = price, Quantity = 1 });
        }

        HttpContext.Session.SetString("cart", JsonSerializer.Serialize(cartItems));
        return RedirectToAction("Index");
    }

    // Remove um item do carrinho
    [HttpPost]
    public IActionResult RemoveFromCart(string productName)
    {
        var cart = HttpContext.Session.GetString("cart");
        var cartItems = string.IsNullOrEmpty(cart) ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(cart);

        var itemToRemove = cartItems.FirstOrDefault(x => x.Name == productName);
        if (itemToRemove != null)
        {
            cartItems.Remove(itemToRemove);
        }

        HttpContext.Session.SetString("cart", JsonSerializer.Serialize(cartItems));
        return RedirectToAction("Index");
    }

    // Limpa o carrinho
    public IActionResult ClearCart()
    {
        HttpContext.Session.Remove("cart");
        return RedirectToAction("Index");
    }
}

// Modelo CartItem
public class CartItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
