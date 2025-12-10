using System.Linq.Expressions;
using zenBlog.domain.Entities;

namespace zenblog.domain.Repositories;

public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync(bool trackChanges=false);

    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);

    IQueryable<TEntity> GetQuery();

    Task<TEntity>? GetByIdAsync(Guid id);

    Task<TEntity>? GetSingleAsync(Expression<Func<TEntity, bool>> filter);

    Task CreateAsync(TEntity  entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);
}
public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}