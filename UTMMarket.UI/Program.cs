using Microsoft.Extensions.DependencyInjection;
using UTMMarket.Application.Interfaces;
using UTMMarket.Application.UseCases;
using UTMMarket.Core.DTOs;
using UTMMarket.Core.Entidades;
using UTMMarket.Core.Interfaces;
using UTMMarket.Infrastructure.Repositories;

var servicios = new ServiceCollection();

// Registro de Repositorios (Mock para ejecución local)
servicios.AddSingleton<IClienteRepository, MockClienteRepository>();
servicios.AddSingleton<IProductoRepository, MockProductoRepository>();
servicios.AddSingleton<IVentaRepository, MockVentaRepository>();

// Registro de Casos de Uso
servicios.AddScoped<IAlertaStockBajoUseCase, AlertaStockBajoUseCaseImpl>();
servicios.AddScoped<IConsultarVentasPorFiltroUseCase, ConsultarVentasPorFiltroUseCaseImpl>();

var proveedorServicios = servicios.BuildServiceProvider();

bool salir = false;
while (!salir)
{
    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine("       UTM Market - Menú Principal");
    Console.WriteLine("========================================");
    Console.WriteLine("1. Gestión de Clientes (Tabla: Clientes)");
    Console.WriteLine("2. Gestión de Inventario (Tabla: Productos)");
    Console.WriteLine("3. Gestión de Ventas (Tablas: Ventas/Detalles)");
    Console.WriteLine("4. Reportes de Alerta (Stock Bajo)");
    Console.WriteLine("5. Consultar Ventas por Fecha");
    Console.WriteLine("6. Salir");
    Console.WriteLine("========================================");
    Console.Write("Seleccione una opción: ");

    var opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1": await MenuClientes(); break;
        case "2": await MenuProductos(); break;
        case "3": await MenuVentas(); break;
        case "4": await ManejarAlertaStock(); break;
        case "5": await ManejarVentasPorFecha(); break;
        case "6": salir = true; break;
    }

    if (!salir)
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
}

async Task MenuClientes()
{
    using var scope = proveedorServicios.CreateScope();
    var repo = scope.ServiceProvider.GetRequiredService<IClienteRepository>();
    
    Console.WriteLine("\n--- OPCIONES DE TABLA: CLIENTES ---");
    Console.WriteLine("1. Registrar Nuevo Cliente");
    Console.WriteLine("2. Listar Clientes");
    var subOpcion = Console.ReadLine();

    if (subOpcion == "1")
    {
        Console.Write("Nombre completo: ");
        var nombre = Console.ReadLine() ?? "";
        Console.Write("Email: ");
        var email = Console.ReadLine() ?? "";
        
        try {
            var cliente = new Cliente { NombreCompleto = nombre, Email = email };
            await repo.AgregarAsync(cliente);
            Console.WriteLine($"Cliente registrado con ID: {cliente.ClienteId}");
        } catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
    }
    else if (subOpcion == "2")
    {
        var clientes = await repo.ObtenerTodosAsync();
        foreach(var c in clientes) Console.WriteLine($"[{c.ClienteId}] {c.NombreCompleto} - {c.Email}");
    }
}

async Task MenuProductos()
{
    using var scope = proveedorServicios.CreateScope();
    var repo = scope.ServiceProvider.GetRequiredService<IProductoRepository>();
    
    Console.WriteLine("\n--- OPCIONES DE TABLA: PRODUCTOS ---");
    Console.WriteLine("1. Listar Productos");
    var subOpcion = Console.ReadLine();

    if (subOpcion == "1")
    {
        await foreach(var p in repo.ObtenerTodosAsync())
            Console.WriteLine($"[{p.ProductoId}] {p.Nombre} - Stock: {p.Stock} - Precio: {p.Precio:C}");
    }
}

async Task MenuVentas()
{
    Console.WriteLine("\n--- OPCIONES DE TABLAS: VENTAS / DETALLES ---");
    Console.WriteLine("1. Consultar Historial Completo");
    // Lógica para ventas...
}

async Task ManejarAlertaStock()
{
    using var scope = proveedorServicios.CreateScope();
    var useCase = scope.ServiceProvider.GetRequiredService<IAlertaStockBajoUseCase>();

    Console.Write("\nUmbral de stock: ");
    if (int.TryParse(Console.ReadLine(), out int umbral))
    {
        await foreach (var p in useCase.EjecutarAsync(umbral))
            Console.WriteLine($"ALERTA: {p.Nombre} tiene solo {p.Stock} unidades.");
    }
}

async Task ManejarVentasPorFecha()
{
    using var scope = proveedorServicios.CreateScope();
    var useCase = scope.ServiceProvider.GetRequiredService<IConsultarVentasPorFiltroUseCase>();

    Console.Write("\nFecha Inicio (aaaa-mm-dd): ");
    DateTime.TryParse(Console.ReadLine(), out DateTime inicio);
    Console.Write("Fecha Fin (aaaa-mm-dd): ");
    DateTime.TryParse(Console.ReadLine(), out DateTime fin);

    var ventas = await useCase.EjecutarAsync(inicio, fin);
    foreach(var v in ventas) Console.WriteLine($"{v.Folio} | {v.Fecha:yyyy-MM-dd} | {v.Total:C}");
}
