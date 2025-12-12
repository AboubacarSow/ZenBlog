using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace zenBlog.infrastructure.Configurations;

public class BlogConfig : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.HasMany(b => b.Comments)
               .WithOne(c => c.Blog)
               .HasForeignKey(c => c.BlogId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}   