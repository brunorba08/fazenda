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

        // Garante que os arquivos estáticos da pasta wwwroot sejam servidos
        app.UseStaticFiles();

        // Ativa o roteamento
        app.UseRouting();

        // Habilita o uso da sessão
        app.UseSession();

        // Middleware de autorização (se necessário)
        app.UseAuthorization();

        // Configura as rotas padrão
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
