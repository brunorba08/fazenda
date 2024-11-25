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

        // Este m�todo � chamado pelo runtime. Use este m�todo para adicionar servi�os ao cont�iner.
        public void ConfigureServices(IServiceCollection services)
        {
            // Usando mem�ria para sess�o
            services.AddDistributedMemoryCache();

            // Configura a sess�o
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expira��o da sess�o
                options.Cookie.HttpOnly = true; // Garante que o cookie n�o seja acess�vel via JavaScript
                options.Cookie.IsEssential = true; // Garante que o cookie seja enviado em todos os requests
            });

            // Adiciona autentica��o baseada em cookies
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login"; // Rota para a p�gina de login
                    options.AccessDeniedPath = "/Account/AccessDenied"; // Rota para p�gina de acesso negado
                });

            // Adiciona os servi�os de controllers e views
            services.AddControllersWithViews();
        }

        // Este m�todo � chamado pelo runtime. Use este m�todo para configurar o pipeline HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // P�gina de exce��o para desenvolvimento
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // P�gina de erro para produ��o
                app.UseHsts(); // Seguran�a para HTTPS
            }

            app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
            app.UseStaticFiles(); // Garante que os arquivos est�ticos da pasta wwwroot sejam servidos

            app.UseRouting(); // Ativa o roteamento

            app.UseSession(); // Middleware de sess�o (antes de autentica��o)

            app.UseAuthentication(); // Middleware para autentica��o
            app.UseAuthorization(); // Middleware de autoriza��o

            // Configura as rotas padr�o
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
