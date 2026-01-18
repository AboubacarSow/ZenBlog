using Microsoft.AspNetCore.Identity;
using zenblog.application.Interfaces;
using zenblog.application.Utilities;

namespace zenblog.application.Authentication.Commands.Login;


public record LoginUserCommand(string UserName, string Password):IRequest<Result<TokenContainer>>;
internal class LoginHandler(IJWTService jwtService, 
UserManager<ApplicationUser> userManager) : IRequestHandler<LoginUserCommand,Result<TokenContainer>>
{
    public async Task<Result<TokenContainer>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.UserName);
        var result = await userManager.CheckPasswordAsync(user!, request.Password);
        if(!result || user is null)
            return Result<TokenContainer>.Failure(Errors.InvalidCredentials);
        var token = await jwtService.CreateTokenAsync(true,user);
        return Result<TokenContainer>.Success(token);
    }
}
