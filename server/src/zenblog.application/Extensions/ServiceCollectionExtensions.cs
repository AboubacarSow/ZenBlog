using Microsoft.Extensions.Configuration;
using zenblog.application.Common.Behaviors;
using zenblog.application.Common.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace zenblog.application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
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

        services.AddScoped<IJWTService, JWTService>();


        var jwtsettings = configuration.GetRequiredSection("JwtSettings");
        string secretKey = jwtsettings["secretKey"]!;
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime=true,
                ValidIssuer = jwtsettings["validIssuer"],
                ValidAudience = jwtsettings["validAudiance"],
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(secretKey))
            };
                
        });
        return services;    
    }
}