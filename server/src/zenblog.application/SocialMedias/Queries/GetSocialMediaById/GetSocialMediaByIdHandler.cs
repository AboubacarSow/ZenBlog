
namespace zenblog.application.SocialMedias.Queries.GetSocialMediaById;

public record GetSocialMediaByIdQuery(Guid Id)
    : IRequest<Result<SocialMediaDto>>;

internal class GetSocialMediaByIdHandler(
    IRepositoryBase<SocialMedia> _repository, IMapper _mapper
) : IRequestHandler<GetSocialMediaByIdQuery, Result<SocialMediaDto>>
{
    public async Task<Result<SocialMediaDto>> Handle(GetSocialMediaByIdQuery query, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(query.Id)!;
        if (entity is null) return Errors.NotFound;

        var dto = _mapper.Map<SocialMediaDto>(entity);
        return Result<SocialMediaDto>.Success(dto);
    }
}


