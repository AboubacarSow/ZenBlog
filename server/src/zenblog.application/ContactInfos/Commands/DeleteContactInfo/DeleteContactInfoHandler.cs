
namespace zenblog.application.ContactInfos.Commands.DeleteContactInfo;

public record DeleteContactInfoCommand(Guid Id): IRequest<Result<Guid>>;

internal class DeleteContactInfoHandler(IRepositoryBase<ContactInfo> _repository, IUnitOfWork _unitOfWork) :
IRequestHandler<DeleteContactInfoCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteContactInfoCommand command, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(command.Id)!;
        if(entity is null) return Errors.NotFound;
        _repository.Delete(entity);
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        if(!result) return Errors.FailedToDelete;
        return Result<Guid>.Success(command.Id);
    }
}
