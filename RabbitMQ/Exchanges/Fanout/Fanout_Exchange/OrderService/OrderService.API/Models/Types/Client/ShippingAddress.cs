namespace OrderService.API.Models.Types.Client;

public class ShippingAddress
{
    public string Street { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public int PostalCode { get; set; }
}