using Microsoft.EntityFrameworkCore;
using zenBlog.domain.Entities;

namespace zenBlog.infrastructure.Persistence;

internal class ZenblogDbContext(DbContextOptions<ZenblogDbContext> options): DbContext(options)
{
    internal DbSet<Blog> Blogs { get; set; } 
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<ContactInfo> ContactInfos { get; set; }
    internal DbSet<Message> Messages { get; set; }
    internal DbSet<SocialMedia> SocialMedias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }

}