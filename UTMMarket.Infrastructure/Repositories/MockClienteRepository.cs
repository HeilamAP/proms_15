using UTMMarket.Core.Entidades;
using UTMMarket.Core.Interfaces;

namespace UTMMarket.Infrastructure.Repositories;

public class MockClienteRepository : IClienteRepository
{
    private readonly List<Cliente> _clientes = [
        new() { ClienteId = 1, NombreCompleto = "Juan Pérez", Email = "juan@example.com" }
    ];

    public Task<Cliente?> ObtenerPorEmailAsync(string email) => 
        Task.FromResult(_clientes.FirstOrDefault(c => c.Email == email));

    public Task AgregarAsync(Cliente cliente)
    {
        cliente.ClienteId = _clientes.Count + 1;
        _clientes.Add(cliente);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Cliente>> ObtenerTodosAsync() => Task.FromResult<IEnumerable<Cliente>>(_clientes);
}
