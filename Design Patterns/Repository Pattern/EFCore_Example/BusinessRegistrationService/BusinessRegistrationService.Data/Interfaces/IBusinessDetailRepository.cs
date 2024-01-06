using BusinessRegistrationService.Data.Entities;

namespace BusinessRegistrationService.Data.Interfaces;

public interface IBusinessDetailRepository : IRepository<BusinessDetailEntity>
{
    Task<BusinessDetailEntity?> QueryAndIncludeAll(Guid id, CancellationToken cancellationToken);
    Task<BusinessDetailEntity> QueryAndIncludeBankAccount(Guid id, CancellationToken cancellationToken);
    Task<BusinessDetailEntity> QueryAndIncludeAccountDetails(Guid id, CancellationToken cancellationToken);
}