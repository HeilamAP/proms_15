using UTMMarket.Core.Entities;
using UTMMarket.Core.DTOs;

namespace UTMMarket.Core.Interfaces;

public interface ISaleRepository
{
    Task<IEnumerable<Sale>> GetByFilterAsync(SaleFilter filter);
}
