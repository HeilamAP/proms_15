using UTMMarket.Application.Interfaces;
using UTMMarket.Core.Entities;
using UTMMarket.Core.DTOs;
using UTMMarket.Core.Interfaces;

namespace UTMMarket.Application.UseCases;

public class FetchSalesByFilterUseCaseImpl(ISaleRepository saleRepository) : IFetchSalesByFilterUseCase
{
    public async Task<IEnumerable<Sale>> ExecuteAsync(SaleFilter filter)
    {
        return await saleRepository.GetByFilterAsync(filter);
    }
}
