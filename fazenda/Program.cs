using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using System;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container
builder.Services.AddControllersWithViews();

// Adiciona suporte a sessões
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo limite da sessão
    options.Cookie.HttpOnly = true; // Protege contra acesso via scripts
    options.Cookie.IsEssential = true; // Necessário mesmo com consentimento de cookies
});

// Configura autenticação baseada em cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Rota para login
        options.AccessDeniedPath = "/Account/AccessDenied"; // Rota para acesso negado
    });

var app = builder.Build();

// Configuração do pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Exibe páginas de erro no ambiente de desenvolvimento
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Rota de erro genérica para produção
    app.UseHsts(); // Segurança adicional para HTTPS
}

app.UseHttpsRedirection(); // Redireciona para HTTPS
app.UseStaticFiles(); // Habilita arquivos estáticos (CSS, JS, etc.)

app.UseRouting(); // Habilita o roteamento

app.UseAuthentication(); // Habilita autenticação
app.UseAuthorization(); // Habilita autorização

app.UseSession(); // Habilita sessões

// Configura rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Inicia o aplicativo
app.Run();
