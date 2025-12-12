using zenblog.application.Messages.Dtos;

namespace zenblog.application.Messages.Queries.GetMessageById;

public record GetMessageByIdQuery(Guid Id): IRequest<Result<MessageDto>>;

internal class GetMessageByIdHandler(IRepositoryBase<Message> _repository, IMapper _mappper) :
IRequestHandler<GetMessageByIdQuery, Result<MessageDto>>
{
    public async Task<Result<MessageDto>> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id)!;
        if(entity is null) return Errors.NotFound;

        var messagedto= _mappper.Map<MessageDto>(entity);
        return Result<MessageDto>.Success(messagedto);
    }
}
