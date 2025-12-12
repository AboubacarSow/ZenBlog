namespace zenblog.application.ContactInfos.Queries.GetContactInfos;

public record GetContactInfosQuery() : IRequest<Result<List<ContactInfoDto>>>;

internal class GetContactInfosHandler(IRepositoryBase<ContactInfo> _repository, IMapper _mapper) : IRequestHandler<GetContactInfosQuery, Result<List<ContactInfoDto>>>
{
    public async Task<Result<List<ContactInfoDto>>> Handle(GetContactInfosQuery request, CancellationToken cancellationToken)
    {
        var contactInfo = await _repository.GetAllAsync(false);
        var contactInfoDto = _mapper.Map<List<ContactInfoDto>>(contactInfo);
        return Result<List<ContactInfoDto>>.Success(contactInfoDto);
    }
}
