

namespace zenblog.infrastructure.Repositories;

internal class RepositoryBase<TEntity>(ZenblogDbContext _context) : IRepositoryBase<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _entity = _context.Set<TEntity>();
    public async Task<List<TEntity>> GetAllAsync(bool trackChanges=false)=>
        await _entity.AsNoTracking().ToListAsync();
    
    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)=>
        await _entity.AsNoTracking().Where(filter).ToListAsync();
    
    public IQueryable<TEntity> GetQuery() => _entity.AsQueryable();
  
    public async Task<TEntity>? GetByIdAsync(Guid id) =>
         (await _entity.FindAsync(id))!;

    public async Task<TEntity>? GetSingleAsync(Expression<Func<TEntity, bool>> filter) =>
         (await _entity.AsNoTracking().FirstOrDefaultAsync(filter))!;  
  
    public async Task CreateAsync(TEntity entity)=> await _entity.AddAsync(entity);

    public void Update(TEntity entity) => _entity.Update(entity);
   
    public void Delete(TEntity entity) => _entity.Remove(entity);
  
}

