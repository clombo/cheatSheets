## [Back to Exchange Page](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges)

## Topic Exchange

### Introduction

It is similar to the direct exchange in that it will route messages where the routing key matches the binding key from the queue binding. However, with a topic exchange, you can also use wildcards in the binding key. When using a topic exchange the routing key of the message must be a list of words separated by dots, like “metrics.server.cpu”. The reason is that topic exchange allows you to match on parts of the routing key and uses dots as separators.

There are two types of wildcard you can use:
- `#`(hash) will match zero or more words, for example, “metrics.#” will match all routing keys that start with “metrics.”
- `*`(star) will match one word, for example, “metrics.*.cpu” will match all routing keys that start with “metrics.” and end in “.cpu”.

### Example Notes

The below example will be implemented using the MassTransit package for .NET. For more information see the [MassTransit Config page.](https://github.com/clombo/cheatSheets/blob/main/RabbitMQ/MassTransit.md)


The [MassTransit Documentation](https://masstransit.io/documentation) will be referenced through out this document as well.

Only the producers/consumers and messages are covered.

Example code can be found [HERE](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Topic/Topic_Exchange)

> **_NOTE:_**  Make sure you have a running instance of RabbitMQ. If you are using different login details update the username and password in AppSettings. See the [MassTransit Config page](https://github.com/clombo/cheatSheets/blob/main/RabbitMQ/MassTransit.md) for more information.

### Characteristics

- Routes messages to 1 or more queues based on the routing key/pattern
- Used for Multicast or publishing to a "group"
- Implements various Pub/Sub Patterns

#### Topic Exchange Diagram:

![RabbitMQ Topic Exchange](https://github.com/clombo/cheatSheets/assets/11086072/6489237a-f746-4256-a8d4-dddb48f577e4)

### Example Implementation using MassTransit

In the example new agreements, like a purchase agreement, can be created either for our store or main office. A agreement service is responsible for creating the agreement and notifying either the office or store service of the agreement.

a 4th Service is a logging service that will log all incoming requests of agreements.

#### Diagram of example implementation.
![Topic Exchange Example](https://github.com/clombo/cheatSheets/assets/11086072/8e66b9ed-926f-4426-a41f-b825befec699)

### Message models/contracts

Same as with the direct exchange Interfaces were used since we have different queues depending on who the agreement is for but the information stays the same.

The "internal" property types I used `Records` as I want it to stay immutable on the producer and consumer side.

For more information about the message contracts in MassTransit see the [messages section on their documentation.](https://masstransit.io/documentation/concepts/messages)

#### `IAgreementDetails.cs`
```cs
using Contracts.Types;

namespace Contracts.Main;

public interface IAgreementDetails
{
    public string AgreementType { get; set; }
    public List<PartyRecord> Parties { get; set; }
    public TermsRecord Terms { get; set; }
    public EnumerationRecord? Enumeration { get; set; }
}
```

#### `IOfficeAgreements.cs`
```cs
using Contracts.Main;

namespace Contracts;

public interface IOfficeAgreements : IAgreementDetails
{
    
}
```

#### `IStoreAgreements.cs`
```cs
using Contracts.Main;

namespace Contracts;

public interface IStoreAgreements : IAgreementDetails
{
    
}
```
Note:
- `IOfficeAgreements.cs` and `IStoreAgreements.cs` inherits from `IAgreementDetails`.
- They are used to make sure the routing is done correctly via the producer.


#### `EnumerationRecord.cs`
```cs
namespace Contracts.Types;

public record EnumerationRecord
{
    public double Price { get; set; }
    public DateTime DueDate { get; set; }
};
```

#### `PartyRecord.cs`
```cs
namespace Contracts.Types;

public record PartyRecord
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactPerson { get; set; }
};
```

#### `TermsRecord.cs`
```cs
namespace Contracts.Types;

public record TermsRecord
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string[] AgreedTerms { get; set; }
    public string[] Deliverables { get; set; }
    public string[] AdditionalInfo { get; set; }
};
```

Each service has a Contracts project containing the above messages/contracts. You can add this to a nuget package for maintainability purposes but in some cases this is seen as a anti-pattern. But if all your services are .NET based like in these examples it might be worth it.

### Services

#### Agreement service

#### `NewAgreementProducer.cs`
```cs
using Contracts;
using MassTransit;
using RabbitMQ.Client;

namespace Bus.Producers;

public static class NewAgreementProducer
{
    public static IRabbitMqBusFactoryConfigurator AddNewAgreementProducer(
        this IRabbitMqBusFactoryConfigurator configurator)
    {
        configurator.Message<IOfficeAgreements>(ct => ct.SetEntityName("AgreementsExchange"));
        configurator.Publish<IOfficeAgreements>(ct => ct.ExchangeType = ExchangeType.Topic);
        configurator.Send<IOfficeAgreements>(
                ct => ct.UseRoutingKeyFormatter(c => "agreements.office")
            );
        
        configurator.Message<IStoreAgreements>(ct => ct.SetEntityName("AgreementsExchange"));
        configurator.Publish<IStoreAgreements>(ct => ct.ExchangeType = ExchangeType.Topic);
        configurator.Send<IStoreAgreements>(
            ct => ct.UseRoutingKeyFormatter(c => "agreements.store")
            );

        return configurator;
    }
}
```
Note:
- Two publishers/producers are created for each message route.
- The publisher is routed to the necessary "topic type" routing key.

#### `Extensions.cs`
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
                        
                        //Producers/Publishers
                        cfg.AddNewAgreementProducer();
                    }
                );
            }
        );
        return services;
    }
}
```
#### `AgreementController.cs`
```cs
using AgreementsService.API.Models;
using AgreementsService.API.Models.MessageDto;
using AutoMapper;
using Contracts;
using Contracts.Types;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace AgreementsService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AgreementController : ControllerBase
{
    
    private readonly ILogger<AgreementController> _logger;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _endpoint;
    
    public AgreementController(ILogger<AgreementController> logger, IMapper mapper, IPublishEndpoint endpoint)
    {
        _logger = logger;
        _mapper = mapper;
        _endpoint = endpoint;
    }

    [HttpPost(Name = "CaptureNewAgreement")]
    public async Task<IActionResult> Post([FromBody] AgreementsModel agreement)
    {
        var message = _mapper.Map<AgreementDetails>(agreement);
        switch (agreement.For)
        {
            case ForCompany.Office:
                await _endpoint.Publish<IOfficeAgreements>(message);
                break;
            case ForCompany.Store:
                await _endpoint.Publish<IStoreAgreements>(message);
                break;
        }
        return Ok();
    }
}
```
Note:
- The message gets mapped and created beforehand since the information is the same, it is just who receives it that differs.
- The switch checks if it is for the office or store and publishes the message accordingly.


#### Office service

#### `NewOfficeAgreementConsumer.cs`
```cs
using AutoMapper;
using Bus.Dtos;
using Contracts;
using MassTransit;

namespace Bus.Consumers;

public class NewOfficeAgreementConsumer : IConsumer<IOfficeAgreements>
{
    private readonly IMapper _mapper;
    public NewOfficeAgreementConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<IOfficeAgreements> context)
    {
        var message = _mapper.Map<OfficeAgreementsDto>(context.Message);
        Console.WriteLine($"--> New office agreement: {message}");
    }
    
}
```
#### `OfficeAgreementQueue.cs`
```cs
using Bus.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace Bus.Queues;

public static class OfficeAgreementQueues
{
    public static IRabbitMqBusFactoryConfigurator AddOfficeAgreementQueues(this IRabbitMqBusFactoryConfigurator configurator, IBusRegistrationContext context)
    {
        configurator.ReceiveEndpoint("office-agreements-queue", ce =>
        {
            ce.ConfigureConsumeTopology = false;
            ce.ConcurrentMessageLimit = 10;
            ce.ConfigureConsumer<NewOfficeAgreementConsumer>(context);
            ce.Bind("AgreementsExchange", cb =>
            {
                cb.RoutingKey = "agreements.office";
                cb.ExchangeType = ExchangeType.Topic;
            });
        });
        return configurator;
    }
}
```
#### `Extensions.cs`
```cs
﻿using Bus.Consumers;
using Bus.Queues;
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
                //Consumers
                x.AddConsumer<NewOfficeAgreementConsumer>();
                
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
                        
                        //Queues
                        cfg.AddOfficeAgreementQueues(ctx);
                    }
                );
            }
        );
        return services;
    }
}
```

#### Store service

#### `NewStoreAgreementConsumer.cs`
```cs
using AutoMapper;
using Bus.Dtos;
using Contracts;
using MassTransit;

namespace Bus.Consumers;

public class NewStoreAgreementConsumer : IConsumer<IStoreAgreements>
{
    private readonly IMapper _mapper;

    public NewStoreAgreementConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<IStoreAgreements> context)
    {
        var message = _mapper.Map<StoreAgreementsDto>(context.Message);
        Console.WriteLine($"--> New office agreement: {message}");
    }
}
```
#### `StoreAgreementQueue.cs`
```cs
using Bus.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace Bus.Queues;

public static class StoreAgreementQueue
{
    public static IRabbitMqBusFactoryConfigurator AddStoreAgreementQueue(this IRabbitMqBusFactoryConfigurator configurator, IBusRegistrationContext context)
    {
        configurator.ReceiveEndpoint("store-agreements-queue", ce =>
        {
            ce.ConfigureConsumeTopology = false;
            ce.ConcurrentMessageLimit = 10;
            ce.ConfigureConsumer<NewStoreAgreementConsumer>(context);
            ce.Bind("AgreementsExchange", cb =>
            {
                cb.RoutingKey = "agreements.store";
                cb.ExchangeType = ExchangeType.Topic;
            });
        });
        return configurator;
    }
}
```
#### `Extensions.cs`
```cs
﻿using Bus.Consumers;
using Bus.Queues;
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
                //Consumers
                x.AddConsumer<NewStoreAgreementConsumer>();
                
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
                        
                        //Queues
                        cfg.AddStoreAgreementQueue(ctx);
                    }
                );
            }
        );
        return services;
    }
}
```

#### Agreement Event Log service

#### `NewAgreementEventConsumer.cs`
```cs
using AutoMapper;
using Bus.DTOs;
using Contracts;
using MassTransit;

namespace Bus.Consumers;

public class NewAgreementEventConsumer : IConsumer<IOfficeAgreements>, IConsumer<IStoreAgreements>
{
    private readonly IMapper _mapper;

    public NewAgreementEventConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<IOfficeAgreements> context)
    {
        var message = _mapper.Map<AgreementsDto>(context.Message);
        Console.WriteLine($"--> New agreements event logged for Office: {message.AgreementType}");
    }

    public async Task Consume(ConsumeContext<IStoreAgreements> context)
    {
        var message = _mapper.Map<AgreementsDto>(context.Message);
        Console.WriteLine($"--> New agreements event logged for Store: {message.AgreementType}");
    }
}
```
Note:
- a Consumer was setup for both message types to be consumed since it needs to consume all messages for logging purposes.

#### `AgreementEventQueue.cs`
```cs
using Bus.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace Bus.Queus;

public static class AgreementEventQueue
{
    public static IRabbitMqBusFactoryConfigurator AddAgreementEventQueue(
        this IRabbitMqBusFactoryConfigurator configurator, IBusRegistrationContext context)
    {
        configurator.ReceiveEndpoint("agreements-event-queue", ce =>
        {
            ce.ConfigureConsumeTopology = false;
            ce.ConcurrentMessageLimit = 10;
            ce.ConfigureConsumer<NewAgreementEventConsumer>(context);
            ce.Bind("AgreementsExchange", cb =>
            {
                cb.RoutingKey = "agreements.*";
                cb.ExchangeType = ExchangeType.Topic;
            });
        });
        
        return configurator;
    }
}
```
Note:
- The `*` wildcard is used so that it consumes all the incoming agreements for both Store and Office.
- The binding is done on the agreements-event-queue.

#### `Extensions.cs`
```cs
﻿using Bus.Consumers;
using Bus.Queus;
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
                //Consumers
                x.AddConsumer<NewAgreementEventConsumer>();

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

                        //Queues
                        cfg.AddAgreementEventQueue(ctx);
                    }
                );
            }
        );
        return services;
    }
}
```

### References
- [MassTransit Documentation](https://masstransit.io/documentation/concepts)
- [RabbitMQ Exchanges,routing keys and bindings](https://www.cloudamqp.com/blog/part4-rabbitmq-for-beginners-exchanges-routing-keys-bindings.html?gad_source=1&gclid=Cj0KCQiAj_CrBhD-ARIsAIiMxT968bAT2q7IKHeMLJ-ttUXsa1pdhW39c7F7FqpXv_eNtLlzP5NbtSoaAofYEALw_wcB)
- [RabbitMQ Topic Excahnge Explained](https://www.cloudamqp.com/blog/rabbitmq-topic-exchange-explained.html)
- [Example Code](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Topic/Topic_Exchange)

## [Back to Exchange Page](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges)
