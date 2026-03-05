namespace UTMMarket.Core.Entidades;

public class Producto
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string? Categoria { get; set; }
    public bool Activo { get; set; } = true;
}
