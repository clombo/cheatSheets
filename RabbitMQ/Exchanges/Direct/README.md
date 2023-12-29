## [Back to Exchange Page](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges)

## Direct Exchange

### Introduction

Direct exchange is one of the exchanges that are built into RabbitMQ and is probably the simplest of them all. Use a direct exchange when the routing keys are known and you want to perform different tasks based on the routing key.

The direct exchange will route the message to a queue whose binding key matches the routing key of the message exactly.One queue can have multiple bindings to the same exchange with different binding keys.

### Example Notes

The below example will be implemented using the MassTransit package for .NET. For more information see the [MassTransit Config page.](https://github.com/clombo/cheatSheets/blob/main/RabbitMQ/MassTransit.md)

The [MassTransit Documentation](https://masstransit.io/documentation) will be referenced through out this document as well.

Only the producers/consumers and messages are covered.

Example code can be found [HERE](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Direct/Direct_Exchange)

> **_NOTE:_**  Make sure you have a running instance of RabbitMQ. If you are using different login details update the username and password in AppSettings. See the [MassTransit Config page](https://github.com/clombo/cheatSheets/blob/main/RabbitMQ/MassTransit.md) for more information.

### Characteristics
- Will route the message to a queue whose binding key matches the routing key of the message EXACTLY.
- One queue can have multiple bindings to the same exchange with different binding keys.

#### Direct Exchange Diagram:

![RabbitMQ Direct Exchange](https://github.com/clombo/cheatSheets/assets/11086072/99d745f7-6464-44cb-9329-4ea21c1d687d)


### Example Implementation using MassTransit

In this example we will be processing a delivery via bicycle, motorcycle, or car. The dilivery information (published message) will stay the same but the way it gets delivered is different and we will handle this with the correct routing/binding key to the necessary queue.

There will be a 4th queue that will act as the event log that will bind to all 3 routing keys to it. This can be consumed by a service to build up the event log in a database for example.

#### Diagram of example implementation.

![Direct Exchange Example](https://github.com/clombo/cheatSheets/assets/11086072/5c3ad243-69ca-474e-b4bb-e1ee5e5a0606)

### Message models/contracts

Interfaces were used in this example since we want to have different queues for the delivery type but the information stays about the same. Since messages with MassTransit uses the full type name including the namespace I decided to use Interfaces to share contract implementation details more easily and make sure the necessary exchanges and queues still consumes it correctly.

For more information about the message contracts in MassTransit see the [messages section on their documentation.](https://masstransit.io/documentation/concepts/messages)

4 Interfaces were created. The main interface will be `IDeliveryRecord` and the other 3 are `IDeliveryByCar`, `IDeliveryByMotorcycle`, and `IDeliveryByBicycle`. `IDeliveryRecord` is the only one with properties in this example but you can add to them if there is any properties unique to the specific delivery type.

`IDeliveryRecord.cs`
```cs
namespace Contracts;

public interface IDeliveryRecord
{
    public int BookingId { get; set; }
    public string DeliveryAddress { get; set; }
}
```

Create queues and bindings

`IDeliveryByBicycle.cs`
```cs
namespace Contracts;

public interface IDeliveryByBicycle : IDeliveryRecord
{
    
}
```

`IDeliveryByMotorcycle.cs`
```cs
namespace Contracts;

public interface IDeliveryByMotorcycle : IDeliveryRecord
{
    
}
```

`IDeliveryByCar.cs`
```cs
namespace Contracts;

public interface IDeliveryByCar : IDeliveryRecord
{
    
}
```

Each service has a Contracts project containing the above messages/contracts. You can add this to a nuget package for maintainability purposes but in some cases this is seen as a anti-pattern. But if all your services are .NET based like in these examples it might be worth it.

### Services

#### Warehouse service

This is the main service that will be sending/publish the message to RabbitMQ. It is a basic WebAPI with one controller booking a parcel for delivery.

#### `OutForDeliveryProducer.cs`
```cs
using Contracts;
using MassTransit;
using RabbitMQ.Client;

namespace OrderService.Bus.Producers;

public static class OutForDeliveryProducer
{
    public static IRabbitMqBusFactoryConfigurator AddOutForDeliveryProducer(this IRabbitMqBusFactoryConfigurator configurator)
    {
        //Add direct queue publish for bicycle
        configurator.Message<IDeliveryByCar>(ct => ct.SetEntityName("ParcelBookings"));
        configurator.Publish<IDeliveryByCar>(ct => ct.ExchangeType = ExchangeType.Direct);
        configurator.Send<IDeliveryByCar>(
            ct => ct.UseRoutingKeyFormatter(c => "car-delivery")
        );
        
        //Add direct queue publish for motorcycle
        configurator.Message<IDeliveryByBicycle>(ct => ct.SetEntityName("ParcelBookings"));
        configurator.Publish<IDeliveryByBicycle>(ct => ct.ExchangeType = ExchangeType.Direct);
        configurator.Send<IDeliveryByBicycle>(
            ct => ct.UseRoutingKeyFormatter(c => "bicycle-delivery")
        );
        
        //Add direct queue publish for car
        configurator.Message<IDeliveryByMotorcycle>(ct => ct.SetEntityName("ParcelBookings"));
        configurator.Publish<IDeliveryByMotorcycle>(ct => ct.ExchangeType = ExchangeType.Direct);
        configurator.Send<IDeliveryByMotorcycle>(
            ct => ct.UseRoutingKeyFormatter(c => "motorcycle-delivery")
        );
        
        return configurator;
    }
}
```

Few notes on the code above:
- Sets the Exchange name to `ParcelBookings`
- Uses `motorcycle-delivery`,`bicycle-delivery` and `car-delivery` as the Routing/Binding keys
- The `IRabbitMqBusFactoryConfigurator` is used to configure Producers,Consumers, and Queus

#### `Extensions.cs`
```cs
using MassTransit;
using OrderService.Bus.Producers;

namespace OrderService.Bus;

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
                        cfg.AddOutForDeliveryProducer();
                    }
                );
            }
        );
        return services;
    }
}
```

#### `ParcelController.cs`
```cs
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
```

#### Test JSON object

```json
{
  "bookingId": 1,
  "sender": {
    "name": "Asmodaios Rati",
    "address": "2471 Burger St",
    "contactNumber": "082 618 6864"
  },
  "receiver": {
    "name": "Vincentius Gita",
    "address": "1482 Protea St",
    "contact": "083 484 1293"
  },
  "parcelDetails": {
    "weight": 10
  },
  "contents": {
    "description": "Electronics out for delivery"
  },
  "deliveryType": 2,
  "status": "Out for Delivery"
}
```
#### Delivery Types
- 0 Bicycle
- 1 Motorcycle
- 2 Car

You can test the endpoint using swagger at `http://localhost:5190/swagger/index.html`

#### Delivery service

#### `ParcelBookedConsumer.cs`
```cs
using Contracts;
using MassTransit;

namespace DeliveryService.Bus.Consumers;

public class ParcelBookedConsumer : IConsumer<IDeliveryRecord>
{
    public async Task Consume(ConsumeContext<IDeliveryRecord> context)
    {
        try
        {
            Console.Write($"--> Message Received: {context.Message}; On Routing key {context.RoutingKey()}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
```
Notes:
- `IConsumer` takes in the contract/message to be consumed.
- `Consume` method is required method to be implemented.

#### `Queue(s)`
```cs
using DeliveryService.Bus.Consumers;
using MassTransit;
using RabbitMQ.Client;


namespace DeliveryService.Bus.Queues;

public static class BicycleQueue
{
    public static IRabbitMqBusFactoryConfigurator AddBicycleQueue(
        this IRabbitMqBusFactoryConfigurator configuration, IBusRegistrationContext context)
    {
        configuration.ReceiveEndpoint("bicycle-delivery-queue", ce =>
        {
            ce.ConfigureConsumeTopology = false;
            ce.ConcurrentMessageLimit = 10;
            ce.ConfigureConsumer<ParcelBookedConsumer>(context);
            ce.Bind("ParcelBookings", cb =>
            {
                cb.RoutingKey = "bicycle-delivery";
                cb.ExchangeType = ExchangeType.Direct;
            });
        });
        return configuration;
    }
}
```
Notes:
- `bicycle-delivery-queue` is the queue name
- Binded to the `ParcelBookings` Exchange using the `bicycle-delivery` routing/binding key
- Consumer `ParcelBookedConsumer` is the consumer to be configured or linked to the queue/key combination on the Exchange.

There are two more queues for Motorcycle and Car deliveries that look exactly the same except for the queue name and binding key used.

#### `Extensions.cs`
```cs
using DeliveryService.Bus.Consumers;
using DeliveryService.Bus.Queues;
using MassTransit;

namespace DeliveryService.Bus;

public static class Extensions
{
    public static IServiceCollection AddServiceBus(
        this IServiceCollection services,IConfiguration configuration
    )
    {
        services.AddMassTransit(x =>
            {   
                //Consumers
                x.AddConsumer<ParcelBookedConsumer>();
                
                x.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host(
                            configuration["RabbitMQ:HostAddress"], 
                            //configuration["RabbitMQ:ConnectionName"], 
                            c =>
                            {
                                c.Username(configuration["RabbitMQ:Username"]);
                                c.Password(configuration["RabbitMQ:Password"]);
                            }
                        );

                        //Queues
                        cfg.AddBicycleQueue(ctx);
                        cfg.AddMotorcycleQueue(ctx);
                        cfg.AddCarQueue(ctx);
                    }
                );
            }
        );

        return services;
    }
}
```

#### Event service
#### `EventLoggedConsumer`
```cs
using Contracts;
using MassTransit;

namespace EventLogService.Bus.Consumers;

public class EventLoggedConsumer : IConsumer<IDeliveryRecord>
{
    public async Task Consume(ConsumeContext<IDeliveryRecord> context)
    {
        try
        {
            Console.Write($"--> Message Received: {context.Message}; On Routing key {context.RoutingKey()}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
```
#### `Event Queue`
```cs
using EventLogService.Bus.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace EventLogService.Bus.Queues;

public static class EventQueue
{
    public static IRabbitMqBusFactoryConfigurator AddEventQueue(
        this IRabbitMqBusFactoryConfigurator configuration, IBusRegistrationContext context)
    {
        configuration.ReceiveEndpoint("event-queue", ce =>
        {
            ce.ConfigureConsumeTopology = false;
            ce.ConcurrentMessageLimit = 10;
            //ce.UseMessageRetry(r =>  r.Interval(4, TimeSpan.FromSeconds(30)));
            //ce.SetQuorumQueue();
            //ce.SetQueueArgument("declare", "lazy");
            ce.ConfigureConsumer<EventLoggedConsumer>(context);
            ce.Bind("ParcelBookings", cb =>
            {
                cb.RoutingKey = "bicycle-delivery";
                cb.ExchangeType = ExchangeType.Direct;
            });
            ce.Bind("ParcelBookings", cb =>
            {
                cb.RoutingKey = "motorcycle-delivery";
                cb.ExchangeType = ExchangeType.Direct;
            });
            ce.Bind("ParcelBookings", cb =>
            {
                cb.RoutingKey = "car-delivery";
                cb.ExchangeType = ExchangeType.Direct;
            });

        });
        return configuration;
    }
}
```
Notes:

The event queue is binded to all the delivery routing keys so that it will receive all the necessary messages.

#### `Extensions.cs`
```cs
using EventLogService.Bus.Consumers;
using EventLogService.Bus.Queues;
using MassTransit;

namespace EventLogService.Bus;

public static class Extensions
{
    public static IServiceCollection AddServiceBus(
        this IServiceCollection services,IConfiguration configuration
    )
    {
        services.AddMassTransit(x =>
            {   
                //Consumers
                x.AddConsumer<EventLoggedConsumer>();
                
                x.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host(
                            configuration["RabbitMQ:HostAddress"], 
                            //configuration["RabbitMQ:ConnectionName"], 
                            c =>
                            {
                                c.Username(configuration["RabbitMQ:Username"]);
                                c.Password(configuration["RabbitMQ:Password"]);
                            }
                        );

                        //Queues
                        cfg.AddEventQueue(ctx);
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
- [RabbitMQ Direct Excahnge Explained](https://www.cloudamqp.com/blog/rabbitmq-direct-exchange-explained.html)
- [Example Code](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Direct/Direct_Exchange)

## [Back to Exchange Page](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges)
