using BPEDI.Domain.Models.Request;
using BPEDI.Domain.Models.Response;

namespace BPEDI.Domain.Interfaces.Services;

public interface ICashOrderService
{
    Task<StatusUpdateResponse?> UpdateCashOrderStatus(StatusUpdateRequest statusUpdateRequest);
}