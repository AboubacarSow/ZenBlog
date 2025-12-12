namespace zenblog.application.Comments.Commands.CreateComment;

public record CreateCommentCommand(
    string Name,
    string Email,
    string Content, 
    Guid BlogId,
    Guid? ParentCommentId
) : IRequest<Result<Guid>>;

public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        RuleFor(x => x.Content).NotEmpty().WithMessage("Content cannot be empty.");
        RuleFor(x => x.BlogId).NotEmpty().WithMessage("Blog ID must be provided.");
    }
}
internal class CreateCommentHandler(
    IRepositoryBase<Comment> _repository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : IRequestHandler<CreateCommentCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map<Comment>(request);
        if(comment.ParentCommentId != null)
        {
            var parent = await _repository.GetByIdAsync(comment.ParentCommentId.Value)!;
            parent?.Replies.Add(comment);
        }
        await _repository.CreateAsync(comment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(comment.Id);
    }
}
