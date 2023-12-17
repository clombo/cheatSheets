namespace OrderService.API.Models.Types.Product;

public class ProductDetails
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
}