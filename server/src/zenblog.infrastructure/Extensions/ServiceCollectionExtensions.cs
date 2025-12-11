using zenBlog.infrastructure.Intercepters;

namespace zenBlog.infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Infrastructure services registration goes here
        services.AddDbContext<ZenblogDbContext>(options =>
        {
            options.AddInterceptors(new AuditableEntityIntercepter());
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        // Repositories and Unit of Work registration
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}