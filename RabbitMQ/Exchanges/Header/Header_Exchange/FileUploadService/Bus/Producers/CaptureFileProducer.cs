using Contracts;
using MassTransit;
using MassTransit.RabbitMqTransport.Configuration;
using RabbitMQ.Client;

namespace Bus.Producers;

public static class CaptureFileProducer
{
    public static IRabbitMqBusFactoryConfigurator AddCaptureFileProducer(this IRabbitMqBusFactoryConfigurator configurator)
    {
        configurator.Message<IZipFile>(
            ct =>
            {
                ct.SetEntityName("FileUploader");
            });
        configurator.Publish<IZipFile>(
            ct =>
            {
                ct.ExchangeType = ExchangeType.Headers;
            });
        
        configurator.Message<IPdfFile>(
            ct =>
            {
                ct.SetEntityName("FileUploader");
            });
        configurator.Publish<IPdfFile>(
            ct =>
            {
                ct.ExchangeType = ExchangeType.Headers;
            });
        
        
        return configurator;
    }
}