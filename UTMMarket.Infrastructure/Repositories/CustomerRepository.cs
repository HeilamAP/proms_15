using Microsoft.Data.SqlClient;
using UTMMarket.Core.Entities;
using UTMMarket.Core.Interfaces;

namespace UTMMarket.Infrastructure.Repositories;

public class CustomerRepository(string connectionString) : ICustomerRepository
{
    public async Task<Customer?> GetByEmailAsync(string email)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        var query = "SELECT CustomerId, FullName, Email, IsActive FROM Customers WHERE Email = @Email";
        await using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Email", email);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Customer
            {
                CustomerId = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Email = reader.GetString(2),
                IsActive = reader.GetBoolean(3)
            };
        }

        return null;
    }

    public async Task AddAsync(Customer customer)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        var query = "INSERT INTO Customers (FullName, Email, IsActive) VALUES (@FullName, @Email, @IsActive); SELECT SCOPE_IDENTITY();";
        await using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@FullName", customer.FullName);
        command.Parameters.AddWithValue("@Email", customer.Email);
        command.Parameters.AddWithValue("@IsActive", customer.IsActive);

        var id = await command.ExecuteScalarAsync();
        if (id != null)
        {
            customer.CustomerId = Convert.ToInt32(id);
        }
    }
}
