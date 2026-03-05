using System.Collections.Generic;
using UTMMarket.Core.Entidades;

namespace UTMMarket.Application.Interfaces;

public interface IAlertaStockBajoUseCase
{
    IAsyncEnumerable<Producto> EjecutarAsync(int umbral);
}
