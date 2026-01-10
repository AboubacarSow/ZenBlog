
using Microsoft.AspNetCore.Identity;

namespace zenblog.application.Authentication.Commands.RegisterUser;


public record RegisterUserCommand(string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        string Password,
        string ConfirmPassword): IRequest<Result>;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(u=>u.Email)
        .NotEmpty().WithMessage("User Email is requierd")
        .EmailAddress().WithMessage("User Email is requierd");

        RuleFor(u=>u.Password)
        .NotEmpty().WithMessage("Password is requierd")
        .MinimumLength(8)
        .WithMessage("Password must contain at least 8 characters")
        .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
        .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
        .Matches("[0-9]").WithMessage("Password must contain at least one number");

        RuleFor(u=>u.ConfirmPassword)
        .NotEmpty().WithMessage("ConfirmPassword is requierd")
        .Equal(u=>u.Password)
        .WithMessage("ConfirmPassword must be identique to Password");

        RuleFor(u=>u.FirstName)
        .NotEmpty()
        .WithMessage("Please Provide your FirstName");

        RuleFor(u=>u.LastName)
        .NotEmpty().WithMessage("Please Provide your LastName");

        RuleFor(u=>u.PhoneNumber)
        .NotEmpty().WithMessage("Please Provide your Phone Number");
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
            return Result.Failure(new Error(identityResult.ToString(),ErrorType.IdentityResult,identityResult.Errors.Select(e=>e.Description).First()));
        return Result.Success();
    }
}


