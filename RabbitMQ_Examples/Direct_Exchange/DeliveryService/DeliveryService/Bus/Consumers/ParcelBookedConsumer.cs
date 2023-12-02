using Contracts;
using MassTransit;

namespace DeliveryService.Bus.Consumers;

public class ParcelBookedConsumer : IConsumer<DeliveryRecord>
{
    public async Task Consume(ConsumeContext<DeliveryRecord> context)
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