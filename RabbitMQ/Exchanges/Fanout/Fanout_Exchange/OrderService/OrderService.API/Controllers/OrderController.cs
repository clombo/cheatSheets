using AutoMapper;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderService.API.Models;

namespace OrderService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _endpoint;

    public OrderController(ILogger<OrderController> logger, IMapper mapper, IPublishEndpoint endpoint)
    {
        _logger = logger;
        _mapper = mapper;
        _endpoint = endpoint;
    }

    [HttpPost(Name = "CreateOrder")]
    public async Task<IActionResult> Post([FromBody] OrderDetails order)
    {
        Console.WriteLine($"--> Request coming in: {order.ToString()}");
        switch (order.Payment.PaymentMethod)
        {
            case "EFT":
                var eftMessage = _mapper.Map<OrderDetailsRecord<BankingDetailsRecord>>(order);
                Console.WriteLine($"--> Message to be sent: {eftMessage}");
                await _endpoint.Publish(eftMessage);
                break;
            case "CreditCard":
                var ccMessage = _mapper.Map<OrderDetailsRecord<CreditCardDetailsRecord>>(order);
                Console.WriteLine($"--> Message to be sent: {ccMessage}");
                await _endpoint.Publish(ccMessage);
                break;
            default:
                throw new Exception("PaymentMethod not recognized.");
        }
        return Ok();
    }
}