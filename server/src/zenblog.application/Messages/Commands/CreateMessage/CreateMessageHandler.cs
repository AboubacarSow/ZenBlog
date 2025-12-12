namespace zenblog.application.Messages.Commands.CreateMessage;

public record CreateMessageCommand(
     string Name ,
     string Email  ,
     string Subject ,
     string Body 
) : IRequest<Result<Guid>>;

internal class CreateMessageHandler(
    IRepositoryBase<Message> _repository,
    IUnitOfWork _unitOfWork
) : IRequestHandler<CreateMessageCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateMessageCommand command, CancellationToken cancellationToken)
    {
        var entity = new Message
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Email = command.Email,
            Subject = command.Subject,
            Body = command.Body,
        };

        await _repository.CreateAsync(entity);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        if (!result) return Errors.FailedToCreate;

        return Result<Guid>.Success(entity.Id);
    }
}