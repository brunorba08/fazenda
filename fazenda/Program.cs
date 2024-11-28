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

// Configuração do banco de dados (DbContext)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlOptions => sqlOptions.EnableRetryOnFailure()));


// Configuração de autenticação e sessão
builder.Services.AddDistributedMemoryCache(); // Usando cache em memória para sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
    options.Cookie.HttpOnly = true; // Garante que o cookie não seja acessível via JavaScript
    options.Cookie.IsEssential = true; // Cookie essencial para funcionamento da sessão
});

// Configuração de autenticação com Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Rota para login
        options.AccessDeniedPath = "/Account/AccessDenied"; // Rota de acesso negado
    });

// Adiciona suporte a controladores e views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do pipeline de requisições (middleware)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Exibe erros detalhados durante o desenvolvimento
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Rota de erro em produção
    app.UseHsts(); // HSTS (HTTP Strict Transport Security) para garantir HTTPS
}

app.UseHttpsRedirection(); // Redireciona automaticamente para HTTPS
app.UseStaticFiles(); // Ativa o uso de arquivos estáticos da pasta wwwroot

// Configuração do roteamento
app.UseRouting();

// Autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Habilita sessões
app.UseSession();

// Definição das rotas padrão (controlador e ação)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run(); // Inicia a aplicação
