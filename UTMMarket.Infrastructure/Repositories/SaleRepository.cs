using UTMMarket.Core.Entities;
using UTMMarket.Core.DTOs;
using UTMMarket.Core.Interfaces;

namespace UTMMarket.Infrastructure.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly List<Sale> _sales = 
    [
        new() { SaleId = 1, Folio = "V001", Date = DateTime.Now.AddDays(-5), TotalAmount = 1500.50m },
        new() { SaleId = 2, Folio = "V002", Date = DateTime.Now.AddDays(-2), TotalAmount = 250.00m },
        new() { SaleId = 3, Folio = "V003", Date = DateTime.Now, TotalAmount = 500.75m }
    ];

    public Task<IEnumerable<Sale>> GetByFilterAsync(SaleFilter filter)
    {
        return Task.FromResult(_sales.Where(s => s.Date >= filter.StartDate && s.Date <= filter.EndDate));
    }
}
