using Bus.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace Bus.Queues;

public static class StoreAgreementQueue
{
    public static IRabbitMqBusFactoryConfigurator AddStoreAgreementQueue(this IRabbitMqBusFactoryConfigurator configurator, IBusRegistrationContext context)
    {
        configurator.ReceiveEndpoint("store-agreements-queue", ce =>
        {
            ce.ConfigureConsumeTopology = false;
            ce.ConcurrentMessageLimit = 10;
            ce.ConfigureConsumer<NewStoreAgreementConsumer>(context);
            ce.Bind("AgreementsExchange", cb =>
            {
                cb.RoutingKey = "agreements.store";
                cb.ExchangeType = ExchangeType.Topic;
            });
        });
        return configurator;
    }
}