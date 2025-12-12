using zenblog.application.Messages.Dtos;

namespace zenblog.application.Messages.Queries.GetAllMessages;

public record GetMessagesQuery: IRequest<Result<List<MessageDto>>>;
internal class GetAllMessagesHandler(IRepositoryBase<Message> _repository, IMapper _mapper) :
IRequestHandler<GetMessagesQuery, Result<List<MessageDto>>>{
    public async Task<Result<List<MessageDto>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var entities= await _repository.GetAllAsync(false);
        var dtos= _mapper.Map<List<MessageDto>>(entities);
        return Result<List<MessageDto>>.Success(dtos);
    }
}
