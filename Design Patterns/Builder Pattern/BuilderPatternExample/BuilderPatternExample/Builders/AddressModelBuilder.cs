using BuilderPatternExample.Models;

namespace BuilderPatternExample.Builders;

public class AddressModelBuilder
{
    private string _street;
    private string _city;
    private string _zip;
    private string _province;

    private AddressModelBuilder()
    {
        
    }

    public static AddressModelBuilder Empty() => new();

    public AddressModelBuilder Street(string street)
    {
        _street = street;
        return this;
    }
    
    public AddressModelBuilder City(string city)
    {
        _city = city;
        return this;
    }
    
    public AddressModelBuilder Zip(string zip)
    {
        _zip = zip;
        return this;
    }
    
    public AddressModelBuilder Province(string province)
    {
        _province = province;
        return this;
    }

    public AddressModel Build()
    {
        return new AddressModel()
        {
            Street = _street,
            City = _city,
            Zip = _zip,
            Province = _province
        };
    }
}