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