using Contracts;
using MassTransit;

namespace Bus.Consumers;

public class ProcessEftPaymentConsumer: IConsumer<OrderDetailsRecord<BankingDetailsRecord>>
{
    
    public async Task Consume(ConsumeContext<OrderDetailsRecord<BankingDetailsRecord>> context)
    {
        var paymentDetails = context.Message.PaymentDetails;
        Console.WriteLine($"Processing EFT payment with a total value of {context.Message.OrderTotal}, payment details {paymentDetails}");
    }
}