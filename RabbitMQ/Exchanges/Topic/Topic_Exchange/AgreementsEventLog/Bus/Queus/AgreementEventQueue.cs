using Bus.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace Bus.Queus;

public static class AgreementEventQueue
{
    public static IRabbitMqBusFactoryConfigurator AddAgreementEventQueue(
        this IRabbitMqBusFactoryConfigurator configurator, IBusRegistrationContext context)
    {
        configurator.ReceiveEndpoint("agreements-event-queue", ce =>
        {
            ce.ConfigureConsumeTopology = false;
            ce.ConcurrentMessageLimit = 10;
            ce.ConfigureConsumer<NewAgreementEventConsumer>(context);
            ce.Bind("AgreementsExchange", cb =>
            {
                cb.RoutingKey = "agreements.*";
                cb.ExchangeType = ExchangeType.Topic;
            });
        });
        
        return configurator;
    }
}