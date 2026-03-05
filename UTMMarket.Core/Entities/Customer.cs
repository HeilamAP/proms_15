namespace UTMMarket.Core.Entities;

public class Customer
{
    public int CustomerId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    private string _email = string.Empty;
    public string Email 
    { 
        get => field; 
        set 
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                throw new ArgumentException("Invalid email format.");
            field = value;
        } 
    } = string.Empty;
}
