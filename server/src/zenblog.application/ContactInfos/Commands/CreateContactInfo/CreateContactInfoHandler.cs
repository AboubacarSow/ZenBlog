namespace zenblog.application.ContactInfos.Commands.CreateContactInfo;

public record CreateContactInfoCommand(
    string Address,
    string Email,
    string Phone
) : IRequest<Result<Guid>>;


public class CreateContactInfoValidator : AbstractValidator<CreateContactInfoCommand>
{
    public CreateContactInfoValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.");

        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress().WithMessage("A valid email is required.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.");

       
    }
}


internal class CreateContactInfoHandler(
    IRepositoryBase<ContactInfo> _repository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : IRequestHandler<CreateContactInfoCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateContactInfoCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ContactInfo>(request);

        await _repository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(entity.Id);
    }
}
