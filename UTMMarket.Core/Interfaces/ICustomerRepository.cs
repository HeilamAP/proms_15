using UTMMarket.Core.Entities;

namespace UTMMarket.Core.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByEmailAsync(string email);
    Task AddAsync(Customer customer);
}
