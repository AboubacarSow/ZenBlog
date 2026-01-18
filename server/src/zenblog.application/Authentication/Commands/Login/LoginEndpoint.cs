using zenblog.application.Utilities;

namespace zenblog.application.Authentication.Commands.Login;

public record LoginUserRequest(LoginUserCommand Command);
public class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/authentication/login", async (LoginUserRequest request, ISender sender) =>
        {
            var result = await sender.Send(request.Command);
           if (!result.IsSuccess)
               return Results.BadRequest(result.Data);
            return Results.Ok(result.Data);
        }).WithTags("Authentication")
        .WithName("Login")
        .Produces<TokenContainer>(StatusCodes.Status200OK)
        .Produces<TokenContainer>(StatusCodes.Status400BadRequest);
    }
}