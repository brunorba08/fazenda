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

        // A��o GET para exibir a p�gina de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // A��o POST para processar o login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verificar se o e-mail existe no banco de dados
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

                // Verificar se o usu�rio existe e a senha est� correta
                if (user != null && user.Password == model.Password)
                {
                    // Se o login for bem-sucedido, redirecionar para a p�gina inicial
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Se as credenciais estiverem erradas, adicionar erro ao ModelState
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
                // Verificar se j� existe um usu�rio com o mesmo e-mail
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    // Adicionar um erro ao ModelState se o e-mail j� existir
                    ModelState.AddModelError(string.Empty, "J� existe um usu�rio com esse e-mail.");
                    return View(model);
                }

                // Se o e-mail for �nico, adicionar o novo usu�rio ao banco de dados
                _context.Users.Add(model);
                _context.SaveChanges();

                // Redirecionar para a p�gina de login ap�s o cadastro
                return RedirectToAction("Login");
            }

            return View(model);
        }
    }
}
