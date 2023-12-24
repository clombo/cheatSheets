using AutoMapper;
using Bus.Dtos;
using Contracts;
using MassTransit;

namespace Bus.Consumers;

public class NewStoreAgreementConsumer : IConsumer<IStoreAgreements>
{
    private readonly IMapper _mapper;

    public NewStoreAgreementConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<IStoreAgreements> context)
    {
        var message = _mapper.Map<StoreAgreementsDto>(context.Message);
        Console.WriteLine($"--> New office agreement: {message}");
    }
}