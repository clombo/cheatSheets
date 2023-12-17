using DeliveryService.Bus.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace DeliveryService.Bus.Queues;

public static class MotorcycleQueue
{
    public static IRabbitMqBusFactoryConfigurator AddMotorcycleQueue(
        this IRabbitMqBusFactoryConfigurator configuration, IBusRegistrationContext context)
    {
        configuration.ReceiveEndpoint("motorcycle-delivery-queue", ce =>
        {
            ce.ConfigureConsumeTopology = false;
            ce.ConcurrentMessageLimit = 10;
            //ce.UseMessageRetry(r =>  r.Interval(4, TimeSpan.FromSeconds(30)));
            //ce.SetQuorumQueue();
            //ce.SetQueueArgument("declare", "lazy");
            ce.ConfigureConsumer<ParcelBookedConsumer>(context);
            ce.Bind("ParcelBookings", cb =>
            {
                cb.RoutingKey = "motorcycle-delivery";
                cb.ExchangeType = ExchangeType.Direct;
            });
        });
        return configuration;
    }
}