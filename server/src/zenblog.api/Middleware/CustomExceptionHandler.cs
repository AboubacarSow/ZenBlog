using System.ComponentModel.DataAnnotations;

namespace zenBlog.api.Middleware;

internal class CustomExceptionHandler( ILogger<CustomExceptionHandler> logger): IMiddleware
{
    private readonly ILogger<CustomExceptionHandler> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch(ValidationException exception)
        {
            _logger.LogWarning(exception, "A validation exception occurred.");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
           var response = new 
            { 
                Message = "One or more validation errors occurred.",
                Errors = exception.ValidationResult.MemberNames.Select(name => new { Field = name, Error = exception.ValidationResult.ErrorMessage })
            };
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var response = new { Message = "An unexpected error occurred. Please try again later." };
            await context.Response.WriteAsJsonAsync(response);
        }
    }

}