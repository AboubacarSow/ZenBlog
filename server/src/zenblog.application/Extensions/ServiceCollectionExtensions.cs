using Carter;
using zenblog.application.Common.Behaviors;

namespace zenblog.application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AssemblyReference).Assembly); //auto mapper
        services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);// FluentValidation
        services.AddMediatR(config=>{

            config.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
            // Validation Behavior using FluentValidation 
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                
        });// MediatR
        services.AddCarter(configurator: config =>
        {
            var modules =typeof(AssemblyReference).Assembly
            .GetTypes().Where(t => t.IsAssignableTo(typeof(ICarterModule)))
            .ToArray();
            config.WithModules(modules);    
        });// Carter
        return services;    
    }
}