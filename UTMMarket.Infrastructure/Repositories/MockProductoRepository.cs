using UTMMarket.Core.Entidades;
using UTMMarket.Core.Interfaces;

namespace UTMMarket.Infrastructure.Repositories;

public class MockProductoRepository : IProductoRepository
{
    private readonly List<Producto> _productos = [
        new() { ProductoId = 1, Nombre = "Laptop", Stock = 5, Precio = 1200 },
        new() { ProductoId = 2, Nombre = "Teclado", Stock = 2, Precio = 50 }
    ];

    public async IAsyncEnumerable<Producto> ObtenerTodosAsync()
    {
        foreach(var p in _productos) {
            await Task.Delay(1);
            yield return p;
        }
    }
}
