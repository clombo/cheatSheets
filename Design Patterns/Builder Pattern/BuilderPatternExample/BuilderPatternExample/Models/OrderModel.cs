namespace BuilderPatternExample.Models;

public class OrderModel
{
    public int OrderNumber { get; init; }
    public DateTime CreatedOn { get; init; }
    public AddressModel ShippingAddress { get; init; }
}