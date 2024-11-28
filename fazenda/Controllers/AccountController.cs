using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using fazenda.Data;
using fazenda.Models;
using System.Linq;

namespace fazenda.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Ação de login
        public IActionResult Login(string username, string password)
        {
            // Buscar o usuário pelo email (username)
            var user = _context.Users.FirstOrDefault(u => u.Email == username);

            // Verificar se o usuário existe
            if (user != null)
            {
                var passwordHasher = new PasswordHasher<User>(); // Instância do PasswordHasher
                var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);

                // Se a senha for válida
                if (result == PasswordVerificationResult.Success)
                {
                    // Aqui você pode configurar a autenticação do usuário (como um cookie de autenticação)
                    // Para simplicidade, vamos apenas redirecionar para a página inicial
                    return RedirectToAction("Index", "Home");
                }
            }

            // Caso o login falhe
            ViewBag.Message = "Usuário ou senha incorretos.";
            return View();
        }

        // Ação de cadastro
        [HttpPost]
        public IActionResult Register(User user, string password)
        {
            if (ModelState.IsValid)
            {
                var passwordHasher = new PasswordHasher<User>();  // Instância de PasswordHasher
                var hashedPassword = passwordHasher.HashPassword(user, password); // Hash da senha fornecida

                user.Password = hashedPassword; // Armazenar senha hash no banco
                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");  // Redireciona para a página de login após o cadastro
            }

            return View(user);  // Retorna a view de cadastro se houver erros
        }
    }
}
