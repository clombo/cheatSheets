using Bus.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace Bus.Queues;

public static class PdfFileQueue
{
    public static IRabbitMqBusFactoryConfigurator AddPdfFileQueue(this IRabbitMqBusFactoryConfigurator configurator, 
        IBusRegistrationContext context)
    {
        configurator.ReceiveEndpoint("pdf-report-queue", ce =>
        {
            ce.Consumer<PdfFileConsumer>();
            ce.ConfigureConsumeTopology = false;
            ce.Bind("FileUploader", cb =>
            {
                //Set necessary headers
                //This header makes sure that all binding headers must be present
                cb.SetBindingArgument("x-match","all");
                //Custom headers
                cb.SetBindingArgument("format","pdf");
                cb.SetBindingArgument("type","report");
                cb.ExchangeType = ExchangeType.Headers;
            });
        });
        
        return configurator;
    }
}