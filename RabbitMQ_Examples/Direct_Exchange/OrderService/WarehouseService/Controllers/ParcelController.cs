using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;

namespace OrderService.Controllers;

[ApiController]
[Route("[controller]")]
public class ParcelController : ControllerBase
{
    
    private readonly ILogger<ParcelController> _logger;
    //private readonly OutForDeliveryProducer _producer;
    private readonly IBus _bus;

    public ParcelController(ILogger<ParcelController> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    [HttpPost(Name = "BookParcel")]
    public async Task<IActionResult> BookNewParcel([FromBody] ParcelBooking parcel)
    {
        await _bus.Publish<DeliveryRecord>(new DeliveryRecord()
        {
            BookingId = 123,
            DeliveryAddress = "Testing 1233"
        }, cb =>
        {
            cb.SetRoutingKey("bicycle-delivery");
        });
        return Ok();
    }
}