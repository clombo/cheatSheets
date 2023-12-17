using OrderService.API.Models.Types.Client;
using OrderService.API.Models.Types.Payment;
using OrderService.API.Models.Types.Product;

namespace OrderService.API.Models;

public class OrderDetails
{
    public int OrderId { get; set; }
    public List<ProductDetails> Items { get; set; }
    public double OrderTotal { get; set; }
    public ClientDetails Client { get; set; }
    public PaymentDetails Payment { get; set; }
}