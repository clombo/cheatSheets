using BusinessRegistrationService.Data.Context;
using BusinessRegistrationService.Data.Entities;
using BusinessRegistrationService.Data.Interfaces;

namespace BusinessRegistrationService.Data.Repositories;

public class BusinessDetailRepository : Repository<BusinessDetailEntity>, IBusinessDetailRepository
{
    private readonly AppDbContext _context;
    public BusinessDetailRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<BusinessDetailEntity> QueryAndIncludeAll(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<BusinessDetailEntity> QueryAndIncludeBankAccount(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<BusinessDetailEntity> QueryAndIncludeAccountDetails(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}