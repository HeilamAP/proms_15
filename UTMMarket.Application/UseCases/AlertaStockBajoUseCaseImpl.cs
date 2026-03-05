using System.Collections.Generic;
using UTMMarket.Application.Interfaces;
using UTMMarket.Core.Entidades;
using UTMMarket.Core.Interfaces;

namespace UTMMarket.Application.UseCases;

public class AlertaStockBajoUseCaseImpl(IProductoRepository productoRepository) : IAlertaStockBajoUseCase
{
    public async IAsyncEnumerable<Producto> EjecutarAsync(int umbral)
    {
        await foreach (var p in productoRepository.ObtenerTodosAsync())
        {
            if (p.Stock <= umbral) yield return p;
        }
    }
}
