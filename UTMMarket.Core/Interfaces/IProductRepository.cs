using System.Collections.Generic;
using UTMMarket.Core.Entities;

namespace UTMMarket.Core.Interfaces;

public interface IProductRepository
{
    IAsyncEnumerable<Product> GetAllAsync();
}
