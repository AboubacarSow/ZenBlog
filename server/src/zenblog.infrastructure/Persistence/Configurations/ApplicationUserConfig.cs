using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace zenblog.infrastructure.Persistence.Configurations;

public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasMany(a=>a.Blogs)
        .WithOne(a=>a.Author)
        .HasForeignKey(b=>b.AuthorId)
        .OnDelete(DeleteBehavior.NoAction);

        builder.OwnsOne(u=>u.Address);
        
    }
}