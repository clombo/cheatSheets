using BuilderPatternExample.Models;

namespace BuilderPatternExample.Builders;

public class OrderModelBuilder
{
    private int _number;
    private DateTime _createdOn;

    private OrderModel _order;

    private readonly AddressModelBuilder _addressModelBuilder = AddressModelBuilder.Empty();

    private OrderModelBuilder()
    {
    }

    public static OrderModelBuilder Empty() => new();

    public OrderModelBuilder WithNumber(int number)
    {
        _number = number;
        return this;
    }

    public OrderModelBuilder WithCreatedOn(DateTime createdOn)
    {
        _createdOn = createdOn;
        return this;
    }

    public OrderModelBuilder WithShippingAddress(Action<AddressModelBuilder> action)
    {
        action(_addressModelBuilder);
        return this;
    }

    public OrderModel Build()
    {
        return new OrderModel()
        {
            OrderNumber = _number,
            CreatedOn = _createdOn,
            ShippingAddress = _addressModelBuilder.Build()
        };
    }
}