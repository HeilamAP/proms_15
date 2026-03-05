using UTMMarket.Core.Entidades;

namespace UTMMarket.Core.Interfaces;

public interface IVentaRepository
{
    Task<IEnumerable<Venta>> ObtenerPorFiltroAsync(DateTime inicio, DateTime fin);
}
