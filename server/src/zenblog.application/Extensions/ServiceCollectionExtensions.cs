namespace zenblog.application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AssemblyReference).Assembly);
        services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
        services.AddMediatR(config=>config
                .RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
        return services;    
    }
}