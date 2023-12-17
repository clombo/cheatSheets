namespace OrderService.API.Models.Types.Client;

public class ClientDetails
{
    public int ClientId { get; set; }
    public string ClientName { get; set; }
    public string ClientEmail { get; set; }
    public ShippingAddress ShippingAddress { get; set; }
}