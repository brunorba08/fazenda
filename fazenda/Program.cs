var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços ao contêiner.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura o pipeline de requisições HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Erro para ambientes de produção.
    app.UseHsts(); // Configura o HSTS.
}

app.UseHttpsRedirection(); // Redireciona para HTTPS.
app.UseStaticFiles(); // Permite o uso de arquivos estáticos (CSS, JS, imagens).

app.UseRouting(); // Habilita o roteamento.

app.UseAuthorization(); // Autoriza as requisições.

// Configura a rota padrão para direcionar para o controlador Home e a ação Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Rota padrão (Home/Index)

// Inicia a aplicação.
app.Run();
