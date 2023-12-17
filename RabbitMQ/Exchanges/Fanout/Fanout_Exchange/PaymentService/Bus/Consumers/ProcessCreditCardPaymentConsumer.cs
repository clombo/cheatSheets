using Contracts;
using MassTransit;

namespace Bus.Consumers;

public class ProcessCreditCardPaymentConsumer : IConsumer<OrderDetailsRecord<CreditCardDetailsRecord>>
{

    public async Task Consume(ConsumeContext<OrderDetailsRecord<CreditCardDetailsRecord>> context)
    {
        var paymentDetails = context.Message.PaymentDetails;
        Console.WriteLine($"Processing Credit Card payment with a order total of {context.Message.OrderTotal}, payment details {paymentDetails}");
    }
}