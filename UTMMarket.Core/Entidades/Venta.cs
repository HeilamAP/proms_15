namespace UTMMarket.Core.Entidades;

public class Venta
{
    public int VentaId { get; set; }
    public string Folio { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Now;
    public decimal Total { get; set; }
    public int ClienteId { get; set; }
    public List<DetalleVenta> Detalles { get; set; } = [];
}
