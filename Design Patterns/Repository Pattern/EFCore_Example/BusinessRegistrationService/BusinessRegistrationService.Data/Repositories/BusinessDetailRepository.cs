using BusinessRegistrationService.Data.Context;
using BusinessRegistrationService.Data.Entities;
using BusinessRegistrationService.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessRegistrationService.Data.Repositories;

public class BusinessDetailRepository : Repository<BusinessDetailEntity>, IBusinessDetailRepository
{
    private readonly AppDbContext _context;
    public BusinessDetailRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<BusinessDetailEntity> QueryAndIncludeAll(Guid id, CancellationToken cancellationToken)
    {
        return await _context.BusinessDetails
            .Where(w => w.Id == id)
            .Include(ba => ba.BankAccount)
            .Include(ad => ad.AccountDetails)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<BusinessDetailEntity> QueryAndIncludeBankAccount(Guid id, CancellationToken cancellationToken)
    {
        return await _context.BusinessDetails
            .Where(w => w.Id == id)
            .Include(ba => ba.BankAccount)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<BusinessDetailEntity> QueryAndIncludeAccountDetails(Guid id, CancellationToken cancellationToken)
    {
        return await _context.BusinessDetails
            .Where(w => w.Id == id)
            .Include(ad => ad.AccountDetails)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}