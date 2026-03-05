namespace UTMMarket.Core.Entidades;

public class Cliente
{
    public int ClienteId { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }

    public string Email 
    { 
        get => field; 
        set 
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                throw new ArgumentException("Formato de correo electrónico no válido.");
            field = value;
        } 
    } = string.Empty;
}
