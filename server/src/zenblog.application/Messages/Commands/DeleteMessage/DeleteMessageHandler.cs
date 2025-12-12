namespace zenblog.application.Messages.Commands.DeleteMessage;
public record DeleteMessageCommand(Guid Id) : IRequest<Result<Guid>>;

internal class DeleteMessageHandler(
    IRepositoryBase<Message> _repository,
    IUnitOfWork _unitOfWork
) : IRequestHandler<DeleteMessageCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteMessageCommand command, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(command.Id)!;
        if (entity is null)
            return Errors.NotFound;

        _repository.Delete(entity);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        if (!result)
            return Errors.FailedToDelete;

        return Result<Guid>.Success(command.Id);
    }
}

