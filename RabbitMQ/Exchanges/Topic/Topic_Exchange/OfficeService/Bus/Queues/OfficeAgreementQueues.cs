using Bus.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace Bus.Queues;

public static class OfficeAgreementQueues
{
    public static IRabbitMqBusFactoryConfigurator AddOfficeAgreementQueues(this IRabbitMqBusFactoryConfigurator configurator, IBusRegistrationContext context)
    {
        configurator.ReceiveEndpoint("office-agreements-queue", ce =>
        {
            ce.ConfigureConsumeTopology = false;
            ce.ConcurrentMessageLimit = 10;
            ce.ConfigureConsumer<NewOfficeAgreementConsumer>(context);
            ce.Bind("AgreementsExchange", cb =>
            {
                cb.RoutingKey = "agreements.office";
                cb.ExchangeType = ExchangeType.Topic;
            });
        });
        return configurator;
    }
}