
namespace zenblog.application.Comments.Queries.GetAllComments; 


public class GetAllCommentsQuery : IRequest<Result<List<CommentDto>>>;
internal class GetAllCommentsHandler(IRepositoryBase<Comment> _repository, IMapper _mapper) : IRequestHandler<GetAllCommentsQuery, Result<List<CommentDto>>>
{
    public async Task<Result<List<CommentDto>>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _repository.GetAllAsync(false);
        var commentDtos = _mapper.Map<List<CommentDto>>(comments);
        return Result<List<CommentDto>>.Success(commentDtos);
    }
}
