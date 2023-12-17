using MassTransit;
using RabbitMQ.Client;

namespace Bus.Queues;

public static class NotificationQueue
{
    public static IRabbitMqBusFactoryConfigurator AddNotificationQueue(
        this IRabbitMqBusFactoryConfigurator configurator, IBusRegistrationContext context)
    {
        configurator.ReceiveEndpoint("notification-queue", ce =>
        {
            ce.ConfigureConsumeTopology = false;
            ce.ConcurrentMessageLimit = 10;
            ce.ConfigureConsumers(context);
            ce.Bind("OrderExchange", cb =>
            {
                cb.ExchangeType = ExchangeType.Fanout;
            });
        });
        
        return configurator;
    }
}