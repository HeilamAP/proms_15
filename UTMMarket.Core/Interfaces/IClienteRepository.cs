using UTMMarket.Core.Entidades;

namespace UTMMarket.Core.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> ObtenerPorEmailAsync(string email);
    Task AgregarAsync(Cliente cliente);
    Task<IEnumerable<Cliente>> ObtenerTodosAsync();
}
