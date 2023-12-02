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