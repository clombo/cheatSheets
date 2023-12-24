using Contracts;
using MassTransit;

namespace Bus.Consumers;

public class PdfFileConsumer : IConsumer<IPdfFile>
{
    public async Task Consume(ConsumeContext<IPdfFile> context)
    {
        Console.WriteLine($"--> New PDF file inbound: {context.Message.Description}");
    }
}