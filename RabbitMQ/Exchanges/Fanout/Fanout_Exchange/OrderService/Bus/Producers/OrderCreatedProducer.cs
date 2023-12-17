using Contracts;
using MassTransit;
using ExchangeType = RabbitMQ.Client.ExchangeType;

namespace Bus.Producers;

public static class OrderCreatedProducer
{
    public static IRabbitMqBusFactoryConfigurator AddOrderCreatedProducer(this IRabbitMqBusFactoryConfigurator configurator)
    {
        configurator.Message<OrderDetailsRecord<CreditCardDetailsRecord>>(ct => ct.SetEntityName("OrderExchange"));
        configurator.Publish<OrderDetailsRecord<CreditCardDetailsRecord>>(ct => ct.ExchangeType = ExchangeType.Fanout);
        
        configurator.Message<OrderDetailsRecord<BankingDetailsRecord>>(ct => ct.SetEntityName("OrderExchange"));
        configurator.Publish<OrderDetailsRecord<BankingDetailsRecord>>(ct => ct.ExchangeType = ExchangeType.Fanout);
        
        return configurator;
    }
}