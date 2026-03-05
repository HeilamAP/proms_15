using UTMMarket.Application.Interfaces;
using UTMMarket.Core.Entidades;
using UTMMarket.Core.Interfaces;

namespace UTMMarket.Application.UseCases;

public class ConsultarVentasPorFiltroUseCaseImpl(IVentaRepository ventaRepository) : IConsultarVentasPorFiltroUseCase
{
    public async Task<IEnumerable<Venta>> EjecutarAsync(DateTime inicio, DateTime fin)
    {
        return await ventaRepository.ObtenerPorFiltroAsync(inicio, fin);
    }
}
