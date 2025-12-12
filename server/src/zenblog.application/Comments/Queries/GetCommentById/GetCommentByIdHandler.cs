namespace zenblog.application.Comments.Queries.GetCommentById;

public record GetCommentByIdQuery(Guid CommentId) : IRequest<Result<CommentDto>>;
public class GetCommentByIdValidator : AbstractValidator<GetCommentByIdQuery>
{
    public GetCommentByIdValidator()
    {
        RuleFor(x => x.CommentId).NotEmpty().WithMessage("Comment ID must be provided.");
    }
}
internal class GetCommentByIdHandler(IRepositoryBase<Comment> _repository, IMapper _mapper) : IRequestHandler<GetCommentByIdQuery, Result<CommentDto>>
{
    public async Task<Result<CommentDto>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var comment = await _repository.GetByIdAsync(request.CommentId)!;
        if (comment == null)
        {
            return Errors.NotFound;
        }
        var commentDto = _mapper.Map<CommentDto>(comment);
        return Result<CommentDto>.Success(commentDto);
    }
}
