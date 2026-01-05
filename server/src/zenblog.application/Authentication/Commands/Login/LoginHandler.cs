using zenblog.application.Common.Utilities;

namespace zenblog.application.Authentication.Commands.Login;


public record LoginUserCommand(string UserName, string Password):IRequest<Result<TokenDto>>;
internal class LoginHandler : IRequestHandler<LoginUserCommand, Result<TokenDto>>
{
    public Task<Result<TokenDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
