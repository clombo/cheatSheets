using Contracts;
using MassTransit;
using RabbitMQ.Client;

namespace OrderService.Bus.Producers;

public static class OutForDeliveryProducer
{
    public static IRabbitMqBusFactoryConfigurator AddOutForDeliveryProducer(this IRabbitMqBusFactoryConfigurator configurator)
    {
        //Add direct queue publish for bicycle
        configurator.Message<IDeliveryByCar>(ct => ct.SetEntityName("ParcelBookings"));
        configurator.Publish<IDeliveryByCar>(ct => ct.ExchangeType = ExchangeType.Direct);
        configurator.Send<IDeliveryByCar>(
            ct => ct.UseRoutingKeyFormatter(c => "car-delivery")
        );
        
        //Add direct queue publish for motorcycle
        configurator.Message<IDeliveryByBicycle>(ct => ct.SetEntityName("ParcelBookings"));
        configurator.Publish<IDeliveryByBicycle>(ct => ct.ExchangeType = ExchangeType.Direct);
        configurator.Send<IDeliveryByBicycle>(
            ct => ct.UseRoutingKeyFormatter(c => "bicycle-delivery")
        );
        
        //Add direct queue publish for car
        configurator.Message<IDeliveryByMotorcycle>(ct => ct.SetEntityName("ParcelBookings"));
        configurator.Publish<IDeliveryByMotorcycle>(ct => ct.ExchangeType = ExchangeType.Direct);
        configurator.Send<IDeliveryByMotorcycle>(
            ct => ct.UseRoutingKeyFormatter(c => "motorcycle-delivery")
        );
        
        return configurator;
    }
}