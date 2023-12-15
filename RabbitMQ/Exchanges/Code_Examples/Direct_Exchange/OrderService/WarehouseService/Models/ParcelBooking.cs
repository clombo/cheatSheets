using OrderService.Models.Enums;
using OrderService.Models.Types;

namespace OrderService.Models;

public class ParcelBooking
{
    public int BookingId { get; set; }
    public Sender Sender { get; set; }
    public Receiver Receiver { get; set; }
    public Details ParcelDetails { get; set; }
    public Contents Contents { get; set; }
    public DeliveryTypeEnum DeliveryType { get; set; }
    public string Status { get; set; }
}