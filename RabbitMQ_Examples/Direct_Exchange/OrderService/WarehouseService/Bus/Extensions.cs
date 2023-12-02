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