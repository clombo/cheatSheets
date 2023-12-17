using Contracts;
using MassTransit;

namespace DeliveryService.Bus.Consumers;

public class ParcelBookedConsumer : IConsumer<IDeliveryRecord>
{
    public async Task Consume(ConsumeContext<IDeliveryRecord> context)
    {
        try
        {
            Console.Write($"--> Message Received: {context.Message}; On Routing key {context.RoutingKey()}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}