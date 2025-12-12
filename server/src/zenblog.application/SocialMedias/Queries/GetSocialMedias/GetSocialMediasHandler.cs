namespace zenblog.application.SocialMedias.Queries.GetSocialMedias;

public record GetSocialMediasQuery() : IRequest<Result<IEnumerable<SocialMediaDto>>>;
internal class GetSocialMediasHandler(
    IRepositoryBase<SocialMedia> _repository, IMapper _mapper
) : IRequestHandler<GetSocialMediasQuery, Result<IEnumerable<SocialMediaDto>>>
{
    public async Task<Result<IEnumerable<SocialMediaDto>>> Handle(GetSocialMediasQuery query, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(false);

        var dtos = _mapper.Map<List<SocialMediaDto>>(items);

        return Result<IEnumerable<SocialMediaDto>>.Success(dtos);
    }
}

