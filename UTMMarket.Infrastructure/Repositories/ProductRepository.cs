using UTMMarket.Core.Entities;
using UTMMarket.Core.Interfaces;

namespace UTMMarket.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = 
    [
        new() { ProductId = 1, Name = "Laptop", Stock = 5, Price = 1200 },
        new() { ProductId = 2, Name = "Mouse", Stock = 50, Price = 25 },
        new() { ProductId = 3, Name = "Keyboard", Stock = 3, Price = 80 },
        new() { ProductId = 4, Name = "Monitor", Stock = 10, Price = 300 }
    ];

    public async IAsyncEnumerable<Product> GetAllAsync()
    {
        foreach (var product in _products)
        {
            await Task.Delay(10); // Simulating async
            yield return product;
        }
    }
}
