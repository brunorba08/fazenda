using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using fazenda.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração de cache em memória
builder.Services.AddDistributedMemoryCache();

// Configuração de sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
    options.Cookie.HttpOnly = true; // Protege o cookie contra ataques XSS
    options.Cookie.IsEssential = true; // Necessário para funcionalidade crítica
});

// Configuração de autenticação baseada em cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Caminho para a tela de login
        options.AccessDeniedPath = "/Account/AccessDenied"; // Caminho para acesso negado
    });

// Adiciona suporte para controladores e visualizações
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do ambiente
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Configuração de segurança para HTTPS
}

// Middleware do pipeline de requisições
app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve arquivos estáticos (como CSS, JS, imagens)

app.UseRouting();
app.UseSession(); // Habilita o uso de sessões
app.UseAuthentication(); // Habilita autenticação
app.UseAuthorization(); // Habilita autorização

// Configuração de rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Inicializa a aplicação
app.Run();
