using BPEDI.Domain.Dtos.Builders;
using BPEDI.Domain.Interfaces.Repositories;
using BPEDI.Domain.Interfaces.Services;
using BPEDI.Domain.Models.Request;
using BPEDI.Domain.Models.Response;


namespace BPEDI.Application.Services;

public class CashOrderService : ICashOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CashOrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StatusUpdateResponse?> UpdateCashOrderStatus(StatusUpdateRequest statusUpdateRequest)
    {
         var test1 = StatusUpdateBuilder
            .MapFrom(statusUpdateRequest,_unitOfWork)
            .ValidateAndBuild(valid =>
                {
                    valid.ReferenceMustExist();
                    valid.ReferenceInCorrectState();
                }
            );
         
        
        var test2 = StatusUpdateBuilder
            .Create(_unitOfWork)
            .WithReferenceNumber("Test")
            .Build();
        
        return new StatusUpdateResponse();
    }
}