namespace Contracts;

public interface IDeliveryRecord
{
    public int BookingId { get; set; }
    public string DeliveryAddress { get; set; }
}