using Microsoft.AspNetCore.Mvc;  // Necessário para utilizar IActionResult, Controller, etc.
using fazenda.Models;  // Importando o modelo User que foi criado

namespace fazenda.Controllers
{
	// Definindo o controller para as ações relacionadas à conta
	public class AccountController : Controller
	{
		// Ação GET para exibir a página de login
		[HttpGet]
		public IActionResult Login()
		{
			return View();  // Retorna a view de login, normalmente sem nenhum dado inicial
		}

		// Ação POST para processar o login
		[HttpPost]
		public IActionResult Login(User model)
		{
			if (ModelState.IsValid)  // Verifica se o modelo é válido (validação das anotações)
			{
				// Lógica de autenticação (aqui você pode substituir com uma consulta ao banco de dados)
				if (model.Email == "admin@admin.com" && model.Password == "admin123")
				{
					return RedirectToAction("Index", "Home");  // Redireciona para a página inicial
				}
				else
				{
					// Se as credenciais forem inválidas, adiciona um erro ao modelo
					ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
				}
			}

			// Se o modelo for inválido ou se as credenciais estiverem erradas, retorna a view com o modelo para mostrar erros de validação
			return View(model);
		}
	}
}
