namespace zenblog.application.Comments.Commands.DeleteComment;

public record DeleteCommentCommand(Guid Id) : IRequest<Result>;

public class DeleteCommentValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Comment ID must be provided.");
    }
}
internal class DeleteCommentHandler(
    IRepositoryBase<Comment> _repository,
    IUnitOfWork _unitOfWork
) : IRequestHandler<DeleteCommentCommand, Result>
{
    public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _repository.GetByIdAsync(request.Id)!;
        if (comment == null)
        {
            return Errors.NotFound;
        }
        _repository.Delete(comment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
