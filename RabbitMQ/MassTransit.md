##  MassTransit

All the code examples that follow will be in .NET using the `MassTransit` package.

Install the following packages on your project where you will be configuring your message bus (RabbitMQ):

- `MassTransit.AspNetCore`
- `MassTransit.Extensions.DependencyInjection`
- `MassTransit.RabbitMQ`

> **_NOTE:_**  In message bus "speak" you have `publishers`, `subscribers`, and `queues`. Masstransit has different names for each namely `producers`, `consumers`, `endpoints`

#### Configure MassTransit

First we will need to add the necessary configuration values to your `appsettings.json` file. If you have multiple environments each using different instances add it to the correct `appsettings.{env}.json` file.

```json

"RabbitMQ": {
    "HostAddress": "host address like DNS name or IP of RabbitMQ instance",
    "Username": "Username to connect",
    "Password": "Password to connect",
    "ConnectionName": "Friendly name for connection"
}

```

Create a new .cs file (This example it is `Extensions.cs`) in your project. Depending on your project you might need to install some additional packages on the project.

We will be using .NET DI to configure RabbitMQ. we will be using a static `IServiceCollection` method:

```cs

public static class Extenstions
{
    public static IServiceCollection AddServiceBus(
        this IServiceCollection services,IConfiguration configuration
    )
    {
        services.AddMassTransit(x =>
            {   
                x.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host(
                            configuration["RabbitMQ:HostAddress"], 
                            configuration["RabbitMQ:ConnectionName"], 
                            c =>
                            {
                            c.Username(configuration["RabbitMQ:Username"]);
                            c.Password(configuration["RabbitMQ:Password"]);
                            }
                        );                    
                    }
                );
            }
        );

        return services;
    }
}

```

## References

For more on MassTransit see the following links, videos, and articles

- [MassTransit Documentation](https://masstransit.io/documentation/concepts)
- [MassTransit Samples and Project](https://github.com/orgs/MassTransit/repositories?type=all)
- [Chris Patterson](https://www.youtube.com/@PhatBoyG)