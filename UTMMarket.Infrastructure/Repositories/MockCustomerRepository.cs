using UTMMarket.Core.Entities;
using UTMMarket.Core.Interfaces;

namespace UTMMarket.Infrastructure.Repositories;

public class MockCustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _customers = [];

    public Task<Customer?> GetByEmailAsync(string email)
    {
        return Task.FromResult(_customers.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase)));
    }

    public Task AddAsync(Customer customer)
    {
        customer.CustomerId = _customers.Count + 1;
        _customers.Add(customer);
        return Task.CompletedTask;
    }
}
