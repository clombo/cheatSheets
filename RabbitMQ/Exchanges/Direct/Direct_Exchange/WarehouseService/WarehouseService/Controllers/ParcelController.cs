using AutoMapper;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Models.Enums;

namespace OrderService.Controllers;

[ApiController]
[Route("[controller]")]
public class ParcelController : ControllerBase
{
    
    private readonly ILogger<ParcelController> _logger;
    //private readonly OutForDeliveryProducer _producer;
    private readonly IPublishEndpoint _endpoint;
    private readonly IMapper _mapper;

    public ParcelController(ILogger<ParcelController> logger, IMapper mapper, IPublishEndpoint endpoint)
    {
        _logger = logger;
        _mapper = mapper;
        _endpoint = endpoint;
    }

    [HttpPost(Name = "BookParcel")]
    public async Task<IActionResult> BookNewParcel([FromBody] ParcelBooking parcel)
    {
        var message = _mapper.Map<DeliveryModel>(parcel);
        switch (parcel.DeliveryType)
        {
            case DeliveryTypeEnum.Bicycle:
                await _endpoint.Publish<IDeliveryByBicycle>(message);
                break;
            case DeliveryTypeEnum.Car:
                await _endpoint.Publish<IDeliveryByCar>(message);
                break;
            case DeliveryTypeEnum.Motorcycle:
                await _endpoint.Publish<IDeliveryByMotorcycle>(message);
                break;
        }

        return Ok();
    }
}