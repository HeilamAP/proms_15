using Microsoft.Extensions.DependencyInjection;
using UTMMarket.Application.Interfaces;
using UTMMarket.Application.UseCases;
using UTMMarket.Core.DTOs;
using UTMMarket.Core.Entities;
using UTMMarket.Core.Interfaces;
using UTMMarket.Infrastructure.Repositories;

var services = new ServiceCollection();

// Registration
services.AddSingleton<string>("Server=(localdb)\\mssqllocaldb;Database=UTMMarket;Trusted_Connection=True;");
services.AddScoped<ICustomerRepository, CustomerRepository>(sp => 
    new CustomerRepository(sp.GetRequiredService<string>()));
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<ISaleRepository, SaleRepository>();

services.AddScoped<ILowStockAlertUseCase, LowStockAlertUseCaseImpl>();
services.AddScoped<IFetchSalesByFilterUseCase, FetchSalesByFilterUseCaseImpl>();

var serviceProvider = services.BuildServiceProvider();

bool exit = false;
while (!exit)
{
    Console.Clear();
    Console.WriteLine("=== UTM Market - Sistema de Gestión ===");
    Console.WriteLine("1. Registrar Cliente (Ex 1)");
    Console.WriteLine("2. Alerta de Stock Crítico (Ex 2)");
    Console.WriteLine("3. Consultar ventas por fecha (Ex 3)");
    Console.WriteLine("4. Salir");
    Console.Write("Seleccione una opción: ");

    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            await HandleCustomerRegistration();
            break;
        case "2":
            await HandleLowStockAlert();
            break;
        case "3":
            await HandleSalesByDate();
            break;
        case "4":
            exit = true;
            break;
    }

    if (!exit)
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
}

async Task HandleCustomerRegistration()
{
    using var scope = serviceProvider.CreateScope();
    var repo = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();

    Console.WriteLine("\n--- Registro de Cliente ---");
    Console.Write("Nombre completo: ");
    var name = Console.ReadLine() ?? "";
    Console.Write("Email: ");
    var email = Console.ReadLine() ?? "";

    try
    {
        var customer = new Customer { FullName = name, Email = email };
        // This will fail without a real DB, so we catch it for the demo
        await repo.AddAsync(customer);
        Console.WriteLine($"Cliente registrado con ID: {customer.CustomerId}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

async Task HandleLowStockAlert()
{
    using var scope = serviceProvider.CreateScope();
    var useCase = scope.ServiceProvider.GetRequiredService<ILowStockAlertUseCase>();

    Console.Write("\nIngrese el umbral de stock: ");
    if (int.TryParse(Console.ReadLine(), out int threshold))
    {
        Console.WriteLine("\nProductos con bajo stock:");
        Console.WriteLine("-------------------------");
        await foreach (var product in useCase.ExecuteAsync(threshold))
        {
            Console.WriteLine($"ID: {product.ProductId} | {product.Name} | Stock: {product.Stock}");
        }
    }
    else
    {
        Console.WriteLine("Umbral inválido.");
    }
}

async Task HandleSalesByDate()
{
    using var scope = serviceProvider.CreateScope();
    var useCase = scope.ServiceProvider.GetRequiredService<IFetchSalesByFilterUseCase>();

    Console.Write("\nFecha de Inicio (yyyy-mm-dd): ");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
    {
        Console.WriteLine("Fecha inválida.");
        return;
    }

    Console.Write("Fecha de Fin (yyyy-mm-dd): ");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
    {
        Console.WriteLine("Fecha inválida.");
        return;
    }

    var filter = new SaleFilter(startDate, endDate);
    var sales = await useCase.ExecuteAsync(filter);

    Console.WriteLine("\nHistorial de Ventas:");
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine("{0,-10} | {1,-15} | {2,-10}", "Folio", "Fecha", "Monto Total");
    Console.WriteLine("--------------------------------------------------");

    foreach (var sale in sales)
    {
        Console.WriteLine("{0,-10} | {1,-15:yyyy-MM-dd} | {2,10:C}", sale.Folio, sale.Date, sale.TotalAmount);
    }
}
