using UTMMarket.Core.Entidades;
using UTMMarket.Core.Interfaces;

namespace UTMMarket.Infrastructure.Repositories;

public class MockVentaRepository : IVentaRepository
{
    private readonly List<Venta> _ventas = [
        new() { VentaId = 1, Folio = "V-001", Fecha = DateTime.Now, Total = 1250 }
    ];

    public Task<IEnumerable<Venta>> ObtenerPorFiltroAsync(DateTime inicio, DateTime fin) => 
        Task.FromResult(_ventas.Where(v => v.Fecha >= inicio && v.Fecha <= fin));
}
