using Contracts;
using MassTransit;

namespace Bus.Consumers;

public class NotifyConsumer : IConsumer<OrderDetailsRecord<CreditCardDetailsRecord>>, IConsumer<OrderDetailsRecord<BankingDetailsRecord>>
{
    public async Task Consume(ConsumeContext<OrderDetailsRecord<CreditCardDetailsRecord>> context)
    {
        Console.WriteLine($"Send email to {context.Message.ClientName}: Dear {context.Message.ClientName}, we have received your order and will be processed shortly.");
    }

    public async Task Consume(ConsumeContext<OrderDetailsRecord<BankingDetailsRecord>> context)
    {
        Console.WriteLine($"Send email to {context.Message.ClientName}: Dear {context.Message.ClientName}, we have received your order and will be processed shortly.");
    }
}