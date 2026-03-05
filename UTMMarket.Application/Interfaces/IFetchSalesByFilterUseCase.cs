using UTMMarket.Core.Entities;
using UTMMarket.Core.DTOs;

namespace UTMMarket.Application.Interfaces;

public interface IFetchSalesByFilterUseCase
{
    Task<IEnumerable<Sale>> ExecuteAsync(SaleFilter filter);
}
