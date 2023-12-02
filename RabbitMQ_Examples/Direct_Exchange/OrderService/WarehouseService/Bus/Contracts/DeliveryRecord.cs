using Contracts;

namespace OrderService.Bus.Contracts;

//TODO...What happens if I use IDeliveryRecord instead of these interfaces
public class DeliveryRecord : IDeliveryByCar, IDeliveryByBicycle,IDeliveryByMotorcycle
{
    public int BookingId { get; set; }
    public string DeliveryAddress { get; set; }
}