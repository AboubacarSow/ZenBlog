using zenblog.application.Interfaces;
using zenblog.infrastructure.Persistence.Repositories;
using zenblog.infrastructure.Services;
using zenBlog.infrastructure.Intercepters;

namespace zenBlog.infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Infrastructure services registration goes here
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            // options.SignIn.RequireConfirmedAccount=true;
            options.User.RequireUniqueEmail=true;
            options.Password.RequiredLength=8;
        }).AddEntityFrameworkStores<ZenblogDbContext>()
        .AddDefaultTokenProviders();

        services.AddDbContext<ZenblogDbContext>(options =>
        {
            options.AddInterceptors(new AuditableEntityIntercepter());
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        // Repositories and Unit of Work registration
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJWTService, JWTService>();


        return services;
    }
}