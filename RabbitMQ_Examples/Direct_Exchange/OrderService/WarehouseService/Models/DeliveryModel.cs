using Contracts;

namespace OrderService.Models;

public class DeliveryModel : IDeliveryByBicycle, IDeliveryByCar, IDeliveryByMotorcycle
{
    public int BookingId { get; set; }
    public string DeliveryAddress { get; set; }
}