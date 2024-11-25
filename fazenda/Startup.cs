using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace fazenda
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este método é chamado pelo runtime. Use este método para adicionar serviços ao contêiner.
        public void ConfigureServices(IServiceCollection services)
        {
            // Usando memória para sessão
            services.AddDistributedMemoryCache();

            // Configura a sessão
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
                options.Cookie.HttpOnly = true; // Garante que o cookie não seja acessível via JavaScript
                options.Cookie.IsEssential = true; // Garante que o cookie seja enviado em todos os requests
            });

            // Adiciona autenticação baseada em cookies
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login"; // Rota para a página de login
                    options.AccessDeniedPath = "/Account/AccessDenied"; // Rota para página de acesso negado
                });

            // Adiciona os serviços de controllers e views
            services.AddControllersWithViews();
        }

        // Este método é chamado pelo runtime. Use este método para configurar o pipeline HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Página de exceção para desenvolvimento
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Página de erro para produção
                app.UseHsts(); // Segurança para HTTPS
            }

            app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
            app.UseStaticFiles(); // Garante que os arquivos estáticos da pasta wwwroot sejam servidos

            app.UseRouting(); // Ativa o roteamento

            app.UseSession(); // Middleware de sessão (antes de autenticação)

            app.UseAuthentication(); // Middleware para autenticação
            app.UseAuthorization(); // Middleware de autorização

            // Configura as rotas padrão
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
