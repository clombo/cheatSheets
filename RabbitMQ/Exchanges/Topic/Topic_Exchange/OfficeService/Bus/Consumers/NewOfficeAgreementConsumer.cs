using AutoMapper;
using Bus.Dtos;
using Contracts;
using MassTransit;

namespace Bus.Consumers;

public class NewOfficeAgreementConsumer : IConsumer<IOfficeAgreements>
{
    private readonly IMapper _mapper;
    public NewOfficeAgreementConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<IOfficeAgreements> context)
    {
        var message = _mapper.Map<OfficeAgreementsDto>(context.Message);
        Console.WriteLine($"--> New office agreement: {message}");
    }
    
}