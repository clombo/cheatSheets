using Contracts;
using MassTransit;
using RabbitMQ.Client;

namespace Bus.Producers;

public static class NewAgreementProducer
{
    public static IRabbitMqBusFactoryConfigurator AddNewAgreementProducer(
        this IRabbitMqBusFactoryConfigurator configurator)
    {
        configurator.Message<IOfficeAgreements>(ct => ct.SetEntityName("AgreementsExchange"));
        configurator.Publish<IOfficeAgreements>(ct => ct.ExchangeType = ExchangeType.Topic);
        configurator.Send<IOfficeAgreements>(
                ct => ct.UseRoutingKeyFormatter(c => "agreements.office")
            );
        
        configurator.Message<IStoreAgreements>(ct => ct.SetEntityName("AgreementsExchange"));
        configurator.Publish<IStoreAgreements>(ct => ct.ExchangeType = ExchangeType.Topic);
        configurator.Send<IStoreAgreements>(
            ct => ct.UseRoutingKeyFormatter(c => "agreements.store")
            );

        return configurator;
    }
}