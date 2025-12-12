namespace zenblog.application.ContactInfos.Queries.GetContactInfoById;

public record GetContactInfoByIdQuery(Guid Id) : IRequest<Result<ContactInfoDto>>;

public class GetContactInfoByIdValidator : AbstractValidator<GetContactInfoByIdQuery>
{
    public GetContactInfoByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Contact Info ID must be provided.");
    }
}

public class GetContactInfoByIdHandler(
    IRepositoryBase<ContactInfo> _repository,
    IMapper _mapper
) : IRequestHandler<GetContactInfoByIdQuery, Result<ContactInfoDto>>
{
    public async Task<Result<ContactInfoDto>> Handle(GetContactInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var contactInfo = await _repository.GetByIdAsync(request.Id)!;
        if (contactInfo == null)
        {
            return Errors.NotFound;
        }
        var contactInfoDto = _mapper.Map<ContactInfoDto>(contactInfo);
        return Result<ContactInfoDto>.Success(contactInfoDto);
    }
}
