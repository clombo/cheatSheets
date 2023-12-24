using Bus.Consumers;
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