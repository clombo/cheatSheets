using Bus.Consumers;
using MassTransit;
using MassTransit.RabbitMqTransport.Configuration;
using RabbitMQ.Client;

namespace Bus.Queues;

public static class ProcessPaymentQueue
{
    public static IRabbitMqBusFactoryConfigurator AddProcessPaymentQueue(this IRabbitMqBusFactoryConfigurator  configurator,
        IBusRegistrationContext context)
    {
        configurator.ReceiveEndpoint("payment-queue", ce =>
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