using System.Collections.Generic;
using UTMMarket.Application.Interfaces;
using UTMMarket.Core.Entities;
using UTMMarket.Core.Interfaces;

namespace UTMMarket.Application.UseCases;

public class LowStockAlertUseCaseImpl(IProductRepository productRepository) : ILowStockAlertUseCase
{
    public async IAsyncEnumerable<Product> ExecuteAsync(int threshold)
    {
        await foreach (var product in productRepository.GetAllAsync())
        {
            if (product.Stock <= threshold)
            {
                yield return product;
            }
        }
    }
}
