namespace zenblog.infrastructure.Repositories;

internal class UnitOfWork(ZenblogDbContext context) : IUnitOfWork
{
    public async Task<bool> SaveChangesAsync() =>
        await context.SaveChangesAsync() > 0;
}