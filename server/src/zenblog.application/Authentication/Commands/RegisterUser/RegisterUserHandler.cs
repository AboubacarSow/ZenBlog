
using Microsoft.AspNetCore.Identity;

namespace zenblog.application.Authentication.Commands.RegisterUser;


public record RegisterUserCommand(string FirstName,
        string LastName,
        string UserName,
        string Email,
        string PhoneNumber,
        string Password,
        string ConfirmPassword): IRequest<Result>;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(u=>u.Email)
        .NotEmpty()
        .EmailAddress()
        .WithMessage("User Email is requierd");

        RuleFor(u=>u.Password)
        .MinimumLength(6)
        .WithMessage("Password must contain at least 6 characters");

        RuleFor(u=>u.ConfirmPassword)
        .Equal(u=>u.Password)
        .WithMessage("ConfirmPassword must be identique to Password");

        RuleFor(u=>u.FirstName)
        .NotEmpty()
        .WithMessage("Please Provide your FirstName");

        RuleFor(u=>u.LastName)
        .NotEmpty()
        .WithMessage("Please Provide your LastName");
        RuleFor(u=>u.PhoneNumber)
        .NotEmpty()
        .WithMessage("Please Provide your Phone Number");
    }
}
internal class RegisterUserHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<RegisterUserCommand, Result>
{
    public async  Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser(
            firstname:request.FirstName,
            lastname:request.LastName,
            phonenumber:request.PhoneNumber,
            email:request.Email
            );
        var identityResult = await userManager.CreateAsync(user:user,request.Password);
        if(!identityResult.Succeeded)
            return Result.Failure(Errors.FailedToCreateUser);
        return Result.Success();
    }
}


