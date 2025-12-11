
namespace zenblog.application.Blogs.Commands.DeleteBlog;

public record DeleteBlogCommand(Guid BlogId): IRequest<Result>;
public class DeleteBlogValidator : AbstractValidator<DeleteBlogCommand>
{
    public DeleteBlogValidator()
    {
        RuleFor(b => b.BlogId)
            .NotEmpty()
            .WithMessage("Blog Id is required");
    }
}

public class DeleteBlogHandler(IRepositoryBase<Blog> _repository, IUnitOfWork _unitOfWork) : IRequestHandler<DeleteBlogCommand, Result>
{
    public async Task<Result> Handle(DeleteBlogCommand command, CancellationToken cancellationToken)
    {
        var blog = await _repository.GetByIdAsync(command.BlogId)!;
        if(blog is null) return Errors.NotFound;
        _repository.Delete(blog);
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        if(!result) return Errors.FailedToDelete;
        return Result.Success();
    }
}


