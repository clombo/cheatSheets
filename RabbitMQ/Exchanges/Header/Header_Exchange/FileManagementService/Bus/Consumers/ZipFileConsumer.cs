using Contracts;
using MassTransit;

namespace Bus.Consumers;

public class ZipFileConsumer : IConsumer<IZipFile>
{
    public async Task Consume(ConsumeContext<IZipFile> context)
    {
        Console.WriteLine($"--> New Zip file inbound: {context.Message.Description}");
    }
}