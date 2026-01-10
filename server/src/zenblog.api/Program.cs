using Carter;
using Scalar.AspNetCore;
using zenblog.application.Extensions;
using zenBlog.api.Middleware;
using zenBlog.infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("ZenBlogApp",
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:4200",   // Angular dev
                    "https://localhost:4200"
                ).AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();
app.UseExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.MapCarter();
app.UseHttpsRedirection();
app.UseCors("ZenBlogApp");
app.UseAuthorization();


app.Run();
