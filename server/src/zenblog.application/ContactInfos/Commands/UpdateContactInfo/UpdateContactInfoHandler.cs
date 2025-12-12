
namespace zenblog.application.ContactInfos.Commands.UpdateContactInfo;

public record UpdateContactInfoCommand(
    Guid Id,
    string Address,
    string Email,
    string Phone
) : IRequest<Result>;

public class UpdateContactInfoValidator : AbstractValidator<UpdateContactInfoCommand>
{
    public UpdateContactInfoValidator()
    {
        RuleFor(c=>c.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.");

        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress().WithMessage("A valid email is required.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.");
    }
}
public class UpdateContactInfoHandler(IRepositoryBase<ContactInfo> _repository, IUnitOfWork _unitOfWork) :
IRequestHandler<UpdateContactInfoCommand, Result>
{
    public async Task<Result> Handle(UpdateContactInfoCommand command, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(command.Id)!;
        if(entity is null) return Errors.NotFound;

        entity.Email= command.Email;
        entity.Address= command.Address;
        entity.Phone = command.Phone;
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        if(!result) return Errors.FailedToUpdate;
        return Result.Success();
    }
}
