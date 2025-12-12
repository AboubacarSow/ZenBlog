namespace zenblog.application.SocialMedias.Commands.UpdateSocialMedia;



public record UpdateSocialMediaCommand(
   SocialMediaDto SocialMedia
) : IRequest<Result<Guid>>;

internal class UpdateSocialMediaHandler(
    IRepositoryBase<SocialMedia> _repository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : IRequestHandler<UpdateSocialMediaCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateSocialMediaCommand command, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(command.SocialMedia.Id)!;
        if (entity is null) return Errors.NotFound;

        _mapper.Map(command.SocialMedia, entity);

        _repository.Update(entity);
        var saved = await _unitOfWork.SaveChangesAsync(ct);

        if (!saved) return Errors.FailedToUpdate;

        return Result<Guid>.Success(entity.Id);
    }
}
