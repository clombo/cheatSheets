## [Back to Main](http://www.google.com)

## Fanout Exchange

### Introduction 

The below example will be implemented using the MassTransit package for .NET. For more information see the [MassTransit Config page.]()

[MassTransit Config page](https://github.com/clombo/cheatSheets/blob/main/RabbitMQ/MassTransit.md) for more information.

The [MassTransit Documentation](https://masstransit.io/documentation) will be referenced through out this document as well.

Only the producers/consumers and messages are covered.

Example code can be found [HERE](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Fanout/Fanout_Exchange)

> **_NOTE:_**  Make sure you have a running instance of RabbitMQ. If you are using different login details update the username and password in AppSettings. See the 

### Characteristics

- Delivers messages to all queues that are bound to the exchange
- Ignores routing keys.
- Ideal for broadcasting messages.

#### Fanout Exchange Diagram:

![RabbitMQ Fanout Exchange](https://github.com/clombo/cheatSheets/assets/11086072/f52220ce-6682-4922-968c-ef1aa46de9b0)

### Example Implementation using MassTransit

In this example we will be placing a order through the order service. Once the order is placed a message should be sent to the notification service to notifiy the client that his/her order was received and is being processed.

a Message should also be sent to the payment service so that it knows about the order and it should expect (EFT) or process (Credit Card) a payment for the order.

#### Diagram of example implementation.

![Fanout Exchange Example](https://github.com/clombo/cheatSheets/assets/11086072/c8732085-54e9-42ca-8ace-3328174848dc)

### Message models/contracts

Records were used in this example since the messages gets broadcasted over multiple queues. The only information that changes is the payment option type which will be explained below.

`OrderDetailsRecord.cs`
```cs
﻿namespace Contracts;

public record OrderDetailsRecord<T>
{
    public string ClientName { get; set; }
    public string ClientEmail { get; set; }
    public double OrderTotal { get; set; }
    public string Currency { get; set; }
    public T PaymentDetails { get; set; }
};
```
> **_NOTE:_** Since the payment details can be different depending on the payment type selected a generic is passed in as the payment details. This way the same contract can accept either EFT or Credit Card and the consumer sorts out the rest.

`CreditCardDetailsRecord.cs`
```cs
﻿namespace Contracts;

public record CreditCardDetailsRecord
{
    public int CardNumber { get; set; }
    public string Expire { get; set; }
    public string CardHolderName { get; set; }
}
```

`BankingDetailsRecord.cs`
```cs
﻿namespace Contracts;

public record BankingDetailsRecord
{
    public string AccountName { get; set; }
    public int AccountNumber { get; set; }
    public string Bank { get; set; }
    public string Branch { get; set; }
    public string SwiftCode { get; set; }
};
```

Each service has a Contracts project containing the above messages/contracts. You can add this to a nuget package for maintainability purposes but in some cases this is seen as a anti-pattern. But if all your services are .NET based like in these examples it might be worth it.

### Services

#### Order service

This is the main service that will be sending/publish the message to RabbitMQ. It is a basic WebAPI with one controller submitting a order for processing.

#### `OrderCreatedProducer.cs`
```cs
using Contracts;
using MassTransit;
using ExchangeType = RabbitMQ.Client.ExchangeType;

namespace Bus.Producers;

public static class OrderCreatedProducer
{
    public static IRabbitMqBusFactoryConfigurator AddOrderCreatedProducer(this IRabbitMqBusFactoryConfigurator configurator)
    {
        configurator.Message<OrderDetailsRecord<CreditCardDetailsRecord>>(ct => ct.SetEntityName("OrderExchange"));
        configurator.Publish<OrderDetailsRecord<CreditCardDetailsRecord>>(ct => ct.ExchangeType = ExchangeType.Fanout);
        
        configurator.Message<OrderDetailsRecord<BankingDetailsRecord>>(ct => ct.SetEntityName("OrderExchange"));
        configurator.Publish<OrderDetailsRecord<BankingDetailsRecord>>(ct => ct.ExchangeType = ExchangeType.Fanout);
        
        return configurator;
    }
}
```
Notes:
- It creates a producer for each payment type available.
- Will be sent to the "OrderExchange" Fanout Exchange.

#### `Extension.cs`
```cs
﻿using Bus.Producers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bus;

public static class Extensions
{
    public static IServiceCollection AddBus(this IServiceCollection services, IConfiguration config)
    {
        
        services.AddMassTransit(x =>
            {   
                
                x.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host(
                            config["RabbitMQ:HostAddress"], 
                            //configuration["RabbitMQ:ConnectionName"], 
                            c =>
                            {
                                c.Username(config["RabbitMQ:Username"]);
                                c.Password(config["RabbitMQ:Password"]);
                            }
                        );

                        //Configure publishers/producers
                        cfg.AddOrderCreatedProducer();

                    }
                );
            }
        );
        return services;
    }
}
```

#### `OrderController.cs`
```cs
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
```
Notes:
- The controller checks the payment method that comes (ether EFT or CreditCard). If it doesn't recognize the method it throws an error.
- The incoming request is mapped to the appropriate message
- Message gets published.

#### `Test JSON`
`Credit Card method JSON`
```json
{
  "orderId": 123456,
  "items": [
    {
       "productId": "ABC123",
        "productName": "Example Product 1",
        "quantity": 2,
        "unitPrice": 25.00
    }
  ],
  "orderTotal": 100.00,
  "client": {
    "clientId": 7890,
    "clientName": "John Doe",
    "clientEmail": "john.doe@example.com",
    "shippingAddress": {
      "street": "123 Main St",
      "city": "Cityville",
      "province": "ProvinceA",
      "postalCode": 12345
    }
  },
  "payment": {
    "paymentMethod": "CreditCard",
    "bankingDetails": null,
    "cardDetails": {
      "cardNumber": 12345,
      "expire": "12/25",
      "cardHolderName": "John Doe"
	  }
   	}
}
```
`EFT method JSON`
```json
{
  "orderId": 123456,
  "items": [
    {
       "productId": "ABC123",
        "productName": "Example Product 1",
        "quantity": 2,
        "unitPrice": 25.00
    }
  ],
  "orderTotal": 100.00,
  "client": {
    "clientId": 7890,
    "clientName": "John Doe",
    "clientEmail": "john.doe@example.com",
    "shippingAddress": {
      "street": "123 Main St",
      "city": "Cityville",
      "province": "ProvinceA",
      "postalCode": 12345
    }
  },
  "payment": {
    "paymentMethod": "EFT",
    "bankingDetails": {
      "accountName": "John Doe",
      "accountNumber": 123456789,
      "bank": "Africa Bank",
      "branch": "ProvinceA",
      "swiftCode": "ABCDUS12345"
    },
    "cardDetails": null
  }
}
```
Notes:
- Depending on the paymentMethod on or the other payment details will be null. Example: When EFT is selected `cardDetails` object is null and the `bankingDetails` object is passed.
- You can test the endpoint using swagger at `TODO`

#### Notification service

This service is a dummy service for a notification that will be sent to the client that submitted the order (doesn't sent actual emails or notifications)

#### `Consumers comes here`
```cs
//TODO
```
#### `Queues comes here`
```cs
//TODO
```
#### `Extension.cs comes here`
```cs
//TODO
```

#### Payment service

This is a dummy service to process the payment depending on the payment details that was sent

#### `Consumers comes here`
```cs
//TODO
```
```cs
//TODO
```
#### `Queues comes here`
```cs
//TODO
```
```cs
//TODO
```
#### `Extension.cs comes here`
```cs
//TODO
```

### References
- [MassTransit Documentation](https://masstransit.io/documentation/concepts)
- [RabbitMQ Exchanges,routing keys and bindings](https://www.cloudamqp.com/blog/part4-rabbitmq-for-beginners-exchanges-routing-keys-bindings.html?gad_source=1&gclid=Cj0KCQiAj_CrBhD-ARIsAIiMxT968bAT2q7IKHeMLJ-ttUXsa1pdhW39c7F7FqpXv_eNtLlzP5NbtSoaAofYEALw_wcB)
- [RabbitMQ Fanout Excahnge Explained](https://www.cloudamqp.com/blog/rabbitmq-fanout-exchange-explained.html)
- [Example Code](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Fanout/Fanout_Exchange)

## [Back to Main](https://github.com/clombo/cheatSheets/blob/main/RabbitMQ/Main.md)

