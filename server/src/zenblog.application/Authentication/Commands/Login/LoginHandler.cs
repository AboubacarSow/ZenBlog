using Microsoft.AspNetCore.Identity;
using zenblog.application.Common.Utilities;

namespace zenblog.application.Authentication.Commands.Login;


public record LoginUserCommand(string UserName, string Password):IRequest<Result<TokenDto>>;
internal class LoginHandler(IJWTService jwtService, 
UserManager<ApplicationUser> userManager) : IRequestHandler<LoginUserCommand,Result<TokenDto>>
{
    public async Task<Result<TokenDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.UserName);
        var result = await userManager.CheckPasswordAsync(user!, request.Password);
        if(!result || user is null)
            return Result<TokenDto>.Failure(Errors.InvalidCredentials);
        var token = await jwtService.CreateTokenAsync(true,user);
        return Result<TokenDto>.Success(token);
    }
}
