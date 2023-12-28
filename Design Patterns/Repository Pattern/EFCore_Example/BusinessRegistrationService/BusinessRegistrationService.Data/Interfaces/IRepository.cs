namespace BusinessRegistrationService.Data.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> Query(Guid id, CancellationToken cancellationToken);
    Task<List<TEntity>> QueryAll(CancellationToken cancellationToken);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
}