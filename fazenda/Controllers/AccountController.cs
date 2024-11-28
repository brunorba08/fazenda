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

        // A��o de login
        public IActionResult Login(string username, string password)
        {
            // Buscar o usu�rio pelo email (username)
            var user = _context.Users.FirstOrDefault(u => u.Email == username);

            // Verificar se o usu�rio existe
            if (user != null)
            {
                var passwordHasher = new PasswordHasher<User>(); // Inst�ncia do PasswordHasher
                var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);

                // Se a senha for v�lida
                if (result == PasswordVerificationResult.Success)
                {
                    // Aqui voc� pode configurar a autentica��o do usu�rio (como um cookie de autentica��o)
                    // Para simplicidade, vamos apenas redirecionar para a p�gina inicial
                    return RedirectToAction("Index", "Home");
                }
            }

            // Caso o login falhe
            ViewBag.Message = "Usu�rio ou senha incorretos.";
            return View();
        }

        // A��o de cadastro
        [HttpPost]
        public IActionResult Register(User user, string password)
        {
            if (ModelState.IsValid)
            {
                var passwordHasher = new PasswordHasher<User>();  // Inst�ncia de PasswordHasher
                var hashedPassword = passwordHasher.HashPassword(user, password); // Hash da senha fornecida

                user.Password = hashedPassword; // Armazenar senha hash no banco
                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");  // Redireciona para a p�gina de login ap�s o cadastro
            }

            return View(user);  // Retorna a view de cadastro se houver erros
        }
    }
}
