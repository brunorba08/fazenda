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

        // Garante que os arquivos est�ticos da pasta wwwroot sejam servidos
        app.UseStaticFiles();

        // Ativa o roteamento
        app.UseRouting();

        // Habilita o uso da sess�o
        app.UseSession();

        // Middleware de autoriza��o (se necess�rio)
        app.UseAuthorization();

        // Configura as rotas padr�o
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
