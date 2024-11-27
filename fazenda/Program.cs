using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using System;

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao container
builder.Services.AddControllersWithViews();

// Adiciona suporte a sess�es
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo limite da sess�o
    options.Cookie.HttpOnly = true; // Protege contra acesso via scripts
    options.Cookie.IsEssential = true; // Necess�rio mesmo com consentimento de cookies
});

// Configura autentica��o baseada em cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Rota para login
        options.AccessDeniedPath = "/Account/AccessDenied"; // Rota para acesso negado
    });

var app = builder.Build();

// Configura��o do pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Exibe p�ginas de erro no ambiente de desenvolvimento
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Rota de erro gen�rica para produ��o
    app.UseHsts(); // Seguran�a adicional para HTTPS
}

app.UseHttpsRedirection(); // Redireciona para HTTPS
app.UseStaticFiles(); // Habilita arquivos est�ticos (CSS, JS, etc.)

app.UseRouting(); // Habilita o roteamento

app.UseAuthentication(); // Habilita autentica��o
app.UseAuthorization(); // Habilita autoriza��o

app.UseSession(); // Habilita sess�es

// Configura rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Inicia o aplicativo
app.Run();
