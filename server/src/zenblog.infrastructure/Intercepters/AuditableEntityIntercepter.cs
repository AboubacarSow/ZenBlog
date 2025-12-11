using Microsoft.EntityFrameworkCore.Diagnostics;
using zenBlog.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace zenBlog.infrastructure.Intercepters;


public class AuditableEntityIntercepter : SaveChangesInterceptor
{

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
       UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateEntities(DbContext? context)
    {
        if(context is null) return;

        foreach(var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            var isAdded = entry.State == EntityState.Added;
            var isModified = entry.State == EntityState.Modified || entry.HasChangedOwnedEntities();
            if(entry.Entity is BaseEntity auditableEntity)
            {
                if(isAdded)
                {
                    auditableEntity.CreatedAt = DateTime.UtcNow;
                    entry.Property(x => x.CreatedAt).IsModified = false;
                }
                else if(isModified)
                {
                    entry.Property(x => x.UpdatedAt).IsModified = false;
                    auditableEntity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}