using FeriaAgricultor.Web.Components;
using FeriaAgricultor.Core.Data;
using FeriaAgricultor.Core.Models;
using FeriaAgricultor.Core.Services;
using FeriaAgricultor.Core.Controllers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// 1. PRIMERO: Agregamos servicios al contenedor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// --- AQUÍ ES DONDE DEBEN IR LOS REPOSITORIOS (Antes del Build) ---
builder.Services.AddScoped<IRepositorio<Usuario>>(provider =>
    new RepositorioJson<Usuario>("usuarios.json"));

builder.Services.AddScoped<IRepositorio<Producto>>(provider =>
    new RepositorioJson<Producto>("productos.json"));

builder.Services.AddScoped<IRepositorio<Orden>>(provider =>
    new RepositorioJson<Orden>("ordenes.json"));

builder.Services.AddScoped<FeriaAgricultor.Core.Services.ServicioSesion>();


builder.Services.AddScoped<IServicioEmail, ServicioEmailMock>();


builder.Services.AddScoped<ControladorCarrito>();
// -----------------------------------------------------------------

// 2. SEGUNDO: Ahora sí construimos la app
var app = builder.Build();

// 3. TERCERO: Ejecutamos el Seeder (Esto debe ir después del Build porque usa la 'app' ya creada)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var repoUsuarios = services.GetRequiredService<IRepositorio<Usuario>>();
        var repoProductos = services.GetRequiredService<IRepositorio<Producto>>();
        var repoOrdenes = services.GetRequiredService<IRepositorio<Orden>>();


        InicializadorDatos.Inicializar(repoUsuarios, repoProductos, repoOrdenes);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error al crear datos de prueba: " + ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();