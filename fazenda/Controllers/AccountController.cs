using Microsoft.AspNetCore.Mvc;
using fazenda.Models;  // Para o modelo LoginViewModel e User
using fazenda.Data;    // Para o contexto de dados
using System.Linq;     // Para usar o LINQ, como FirstOrDefault

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

        // Ação GET para exibir a página de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Ação POST para processar o login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verificar se o e-mail existe no banco de dados
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

                // Verificar se o usuário existe e a senha está correta
                if (user != null && user.Password == model.Password)
                {
                    // Se o login for bem-sucedido, redirecionar para a página inicial
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Se as credenciais estiverem erradas, adicionar erro ao ModelState
                    ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
                }
            }
            return View(model);
        }

        // Ação GET para exibir a página de cadastro
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Ação POST para processar o cadastro
        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                // Verificar se já existe um usuário com o mesmo e-mail
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    // Adicionar um erro ao ModelState se o e-mail já existir
                    ModelState.AddModelError(string.Empty, "Já existe um usuário com esse e-mail.");
                    return View(model);
                }

                // Se o e-mail for único, adicionar o novo usuário ao banco de dados
                _context.Users.Add(model);
                _context.SaveChanges();

                // Redirecionar para a página de login após o cadastro
                return RedirectToAction("Login");
            }

            return View(model);
        }
    }
}
