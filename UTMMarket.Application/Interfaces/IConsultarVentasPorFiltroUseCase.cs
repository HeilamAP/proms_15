using UTMMarket.Core.Entidades;

namespace UTMMarket.Application.Interfaces;

public interface IConsultarVentasPorFiltroUseCase
{
    Task<IEnumerable<Venta>> EjecutarAsync(DateTime inicio, DateTime fin);
}
