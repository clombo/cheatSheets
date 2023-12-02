namespace Contracts;

public record DeliveryRecord
{
    public int BookingId { get; set; }
    public string DeliveryAddress { get; set; }
}