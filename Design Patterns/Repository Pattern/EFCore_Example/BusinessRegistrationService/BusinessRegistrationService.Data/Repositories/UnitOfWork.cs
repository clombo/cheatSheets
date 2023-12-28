using BusinessRegistrationService.Data.Context;
using BusinessRegistrationService.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace BusinessRegistrationService.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Lazy<BusinessDetailRepository> _businessDetailRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        _businessDetailRepository =
            new Lazy<BusinessDetailRepository>(() => new BusinessDetailRepository(_context), true);
    }
    
    public IBusinessDetailRepository BusinessDetailRepository => _businessDetailRepository.Value;
    
    public async Task<bool> CompleteAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IDbContextTransaction> BeginDbTransaction()
    {
        return await _context.Database.BeginTransactionAsync();
    }
}