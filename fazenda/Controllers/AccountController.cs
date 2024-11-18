using Microsoft.AspNetCore.Mvc;
using fazenda.Models;  // Importando o modelo User

namespace fazenda.Controllers
{
    public class AccountController : Controller
    {
        // Ação GET para exibir a página de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Ação POST para processar o login
        [HttpPost]
        public IActionResult Login(User model)
        {
            if (ModelState.IsValid)  // Verifica se o modelo é válido (validação das anotações)
            {
                if (model.Email == "admin@admin.com" && model.Password == "admin123")
                {
                    return RedirectToAction("Index", "Home");  // Redireciona para a página inicial
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
                }
            }
            return View(model);
        }

        // Ação GET para exibir a página de cadastro
        [HttpGet]
        public IActionResult Register()
        {
            return View();  // Exibe a página de cadastro
        }

        // Ação POST para processar o cadastro
        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                // Aqui você pode adicionar a lógica para salvar o usuário no banco de dados
                // Exemplo: _context.Users.Add(model); _context.SaveChanges();

                // Após o cadastro, redireciona o usuário para a página de login
                return RedirectToAction("Login");
            }

            // Se o modelo for inválido, retorna a view com o modelo para mostrar erros de validação
            return View(model);
        }
    }
}
