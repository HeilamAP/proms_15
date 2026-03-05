namespace UTMMarket.Core.Entities;

public class Sale
{
    public int SaleId { get; set; }
    public string Folio { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
}
