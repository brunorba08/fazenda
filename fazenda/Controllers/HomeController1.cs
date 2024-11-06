using fazenda.Models;
using Microsoft.AspNetCore.Mvc;

namespace fazenda.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login page
        public IActionResult Login()
        {
            return View();
        }

        // POST: Process login
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                // Validação do usuário (Exemplo: check em banco de dados)
                if (user.Email == "exemplo@gmail.com" && user.Password == "123456")
                {
                    // Redireciona para página principal após login bem-sucedido
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Login inválido!");
            }
            return View(user);
        }
    }

}
