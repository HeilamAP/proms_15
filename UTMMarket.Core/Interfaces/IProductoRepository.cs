using System.Collections.Generic;
using UTMMarket.Core.Entidades;

namespace UTMMarket.Core.Interfaces;

public interface IProductoRepository
{
    IAsyncEnumerable<Producto> ObtenerTodosAsync();
}
