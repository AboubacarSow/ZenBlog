using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace zenblog.infrastructure.Persistence.Configurations;

public class CommentConfig : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasOne(c => c.Blog)
               .WithMany(b => b.Comments)
               .HasForeignKey(c => c.BlogId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.ParentComment)
               .WithMany(c => c.Replies)
               .HasForeignKey(c => c.ParentCommentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
