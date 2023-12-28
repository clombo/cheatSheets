using BusinessRegistrationService.Data.Context;
using BusinessRegistrationService.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessRegistrationService.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<TEntity> Query(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().FindAsync(id, cancellationToken);
    }

    public async Task<List<TEntity>> QueryAll(CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }
        
    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
    }
}