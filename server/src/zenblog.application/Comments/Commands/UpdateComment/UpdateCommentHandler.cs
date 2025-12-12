namespace zenblog.application.Comments.Commands.UpdateComment;
public record UpdateCommentCommand(
    Guid Id,
    string Name,
    string Email,
    string Content,
    Guid BlogId,
    Guid? ParentCommentId
) : IRequest<Result>;

public class UpdateCommentValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Comment ID must be provided.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        RuleFor(x => x.Content).NotEmpty().WithMessage("Content cannot be empty.");
        RuleFor(x => x.BlogId).NotEmpty().WithMessage("Blog ID must be provided.");
        RuleFor(x => x.ParentCommentId).NotEqual(x => x.Id).WithMessage("A comment cannot be its own parent.");
    }
}

internal class UpdateCommentHandler(
    IRepositoryBase<Comment> _repository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : IRequestHandler<UpdateCommentCommand, Result>
{
    public async Task<Result> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _repository.GetByIdAsync(request.Id)!;
        if (comment == null)
        {
            return Errors.NotFound;
        }
        _mapper.Map(request, comment);
        _repository.Update(comment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
