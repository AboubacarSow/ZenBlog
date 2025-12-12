namespace zenblog.application.SocialMedias.Commands.DeleteSocialMedia;

public record DeleteSocialMediaCommand(Guid Id):IRequest<Result<Guid>>;
internal class DeleteSocialMediaHandler(
    IRepositoryBase<SocialMedia> _repository,
    IUnitOfWork _unitOfWork
) : IRequestHandler<DeleteSocialMediaCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteSocialMediaCommand command, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(command.Id)!;
        if (entity is null) return Errors.NotFound;

        _repository.Delete(entity);

        var saved = await _unitOfWork.SaveChangesAsync(cancellationToken);
        if (!saved) return Errors.FailedToDelete;

        return Result<Guid>.Success(command.Id);
    }
}
