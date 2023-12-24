using AutoMapper;
using Bus.DTOs;
using Contracts;
using MassTransit;

namespace Bus.Consumers;

public class NewAgreementEventConsumer : IConsumer<IOfficeAgreements>, IConsumer<IStoreAgreements>
{
    private readonly IMapper _mapper;

    public NewAgreementEventConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<IOfficeAgreements> context)
    {
        var message = _mapper.Map<AgreementsDto>(context.Message);
        Console.WriteLine($"--> New agreements event logged for Office: {message.AgreementType}");
    }

    public async Task Consume(ConsumeContext<IStoreAgreements> context)
    {
        var message = _mapper.Map<AgreementsDto>(context.Message);
        Console.WriteLine($"--> New agreements event logged for Store: {message.AgreementType}");
    }
}