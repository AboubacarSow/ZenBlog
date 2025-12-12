namespace zenblog.application.SocialMedias.Commands.CreateSocialMedia;

public  record CreateSocialMediaCommand(
    string Name,
    string Icon,
    string AddressUrl
) : IRequest<Result<Guid>>;
internal class CreateSocialMediaHandler(
    IRepositoryBase<SocialMedia> _repository,
    IUnitOfWork _unitOfWork
) : IRequestHandler<CreateSocialMediaCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateSocialMediaCommand command, CancellationToken ct)
    {
        var entity = new SocialMedia
        {
            Name= command.Name,
            Icon = command.Icon,
            AddressUrl = command.AddressUrl
        };

        await _repository.CreateAsync(entity);

        var success = await _unitOfWork.SaveChangesAsync(ct);
        if (!success) return Errors.FailedToCreate;

        return Result<Guid>.Success(entity.Id);
    }
}


