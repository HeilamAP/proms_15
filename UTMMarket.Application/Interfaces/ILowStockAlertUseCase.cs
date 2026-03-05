using System.Collections.Generic;
using UTMMarket.Core.Entities;

namespace UTMMarket.Application.Interfaces;

public interface ILowStockAlertUseCase
{
    IAsyncEnumerable<Product> ExecuteAsync(int threshold);
}
