namespace zenblog.application.Authentication.Commands.RegisterUser;
public record RegisterUserRequest(RegisterUserCommand Command);
public class RegisterUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/authentication/register", async (RegisterUserRequest request, ISender sender) =>
        {
            var result = await sender.Send(request.Command);
            if(!result.IsSuccess)
                return Results.BadRequest(error:result.Errors);
            return Results.Created();
        }).WithTags("Authentication")
        .WithName("Register");
    }
}