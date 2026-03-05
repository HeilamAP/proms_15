namespace UTMMarket.Core.Entidades;

public class DetalleVenta
{
    public int DetalleVentaId { get; set; }
    public int VentaId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal => Cantidad * PrecioUnitario;
}
