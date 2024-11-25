using Microsoft.AspNetCore.Mvc;
using fazenda.Models;  // Para o modelo de dados (User)
using fazenda.Data;    // Agora, voc� pode importar o contexto de dados

namespace fazenda.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Construtor para injetar o contexto de dados
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // A��o GET para exibir a p�gina de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // A��o POST para processar o login
        [HttpPost]
        public IActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
                if (user != null && user.Password == model.Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "E-mail ou senha inv�lidos.");
                }
            }
            return View(model);
        }

        // A��o GET para exibir a p�gina de cadastro
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // A��o POST para processar o cadastro
        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError(string.Empty, "J� existe um usu�rio com esse e-mail.");
                    return View(model);
                }

                _context.Users.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(model);
        }
    }
}
