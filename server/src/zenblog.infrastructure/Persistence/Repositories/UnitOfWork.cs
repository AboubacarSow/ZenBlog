namespace zenblog.infrastructure.Repositories;

internal class UnitOfWork(ZenblogDbContext context) : IUnitOfWork
{
    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken) =>
        await context.SaveChangesAsync(cancellationToken) > 0;
}