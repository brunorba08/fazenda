using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System; // Para garantir que TimeSpan seja reconhecido
using fazenda.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do banco de dados (DbContext)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlOptions => sqlOptions.EnableRetryOnFailure()));


// Configura��o de autentica��o e sess�o
builder.Services.AddDistributedMemoryCache(); // Usando cache em mem�ria para sess�o
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expira��o da sess�o
    options.Cookie.HttpOnly = true; // Garante que o cookie n�o seja acess�vel via JavaScript
    options.Cookie.IsEssential = true; // Cookie essencial para funcionamento da sess�o
});

// Configura��o de autentica��o com Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Rota para login
        options.AccessDeniedPath = "/Account/AccessDenied"; // Rota de acesso negado
    });

// Adiciona suporte a controladores e views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura��o do pipeline de requisi��es (middleware)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Exibe erros detalhados durante o desenvolvimento
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Rota de erro em produ��o
    app.UseHsts(); // HSTS (HTTP Strict Transport Security) para garantir HTTPS
}

app.UseHttpsRedirection(); // Redireciona automaticamente para HTTPS
app.UseStaticFiles(); // Ativa o uso de arquivos est�ticos da pasta wwwroot

// Configura��o do roteamento
app.UseRouting();

// Autentica��o e autoriza��o
app.UseAuthentication();
app.UseAuthorization();

// Habilita sess�es
app.UseSession();

// Defini��o das rotas padr�o (controlador e a��o)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run(); // Inicia a aplica��o
