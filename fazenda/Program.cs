using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using fazenda.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura��o de cache em mem�ria
builder.Services.AddDistributedMemoryCache();

// Configura��o de sess�o
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expira��o da sess�o
    options.Cookie.HttpOnly = true; // Protege o cookie contra ataques XSS
    options.Cookie.IsEssential = true; // Necess�rio para funcionalidade cr�tica
});

// Configura��o de autentica��o baseada em cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Caminho para a tela de login
        options.AccessDeniedPath = "/Account/AccessDenied"; // Caminho para acesso negado
    });

// Adiciona suporte para controladores e visualiza��es
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura��o do ambiente
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Configura��o de seguran�a para HTTPS
}

// Middleware do pipeline de requisi��es
app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve arquivos est�ticos (como CSS, JS, imagens)

app.UseRouting();
app.UseSession(); // Habilita o uso de sess�es
app.UseAuthentication(); // Habilita autentica��o
app.UseAuthorization(); // Habilita autoriza��o

// Configura��o de rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Inicializa a aplica��o
app.Run();
