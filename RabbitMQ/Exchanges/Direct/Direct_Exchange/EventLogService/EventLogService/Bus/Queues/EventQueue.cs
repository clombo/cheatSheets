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