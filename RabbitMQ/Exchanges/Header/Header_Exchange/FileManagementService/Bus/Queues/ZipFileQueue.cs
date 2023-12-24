using Bus.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace Bus.Queues;

public static class ZipFileQueue
{
    public static IRabbitMqBusFactoryConfigurator AddZipFileQueue(this IRabbitMqBusFactoryConfigurator configurator, 
        IBusRegistrationContext context)
    {
        
        configurator.ReceiveEndpoint("zip-log-queue", ce =>
        {
            ce.Consumer<ZipFileConsumer>();
            ce.ConfigureConsumeTopology = false;
            ce.Bind("FileUploader", cb =>
            {
                //Set necessary headers
                //This header makes sure that all binding headers must be present
                cb.SetBindingArgument("x-match","all");
                //Custom headers
                cb.SetBindingArgument("format","zip");
                cb.SetBindingArgument("type","log");
                cb.ExchangeType = ExchangeType.Headers;
            });
        });
        
        return configurator;
    }
}