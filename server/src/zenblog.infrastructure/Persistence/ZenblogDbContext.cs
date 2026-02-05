namespace zenblog.infrastructure.Persistence;

internal class ZenblogDbContext(DbContextOptions<ZenblogDbContext> options):
 IdentityDbContext<ApplicationUser,IdentityRole<Guid>, Guid>(options)
{
    internal DbSet<Blog> Blogs { get; set; } = default!;
    internal DbSet<Category> Categories { get; set; }= default!;
    internal DbSet<ContactInfo> ContactInfos { get; set; } = default!;
    internal DbSet<Message> Messages { get; set; } = default!;
    internal DbSet<SocialMedia> SocialMedias { get; set; } = default!;

    internal DbSet<Comment> Comments { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
    }

}
