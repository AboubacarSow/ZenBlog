namespace zenblog.application.Blogs.Commands.UpdateBlog;

public record UpdateBlogCommand(
    Guid BlogId,
    string Title,
    string Content,
    string ImageUrl,
    string CoverImageUrl,
    Guid CategoryId
) : IRequest<Result<Guid>>
{
    public Guid? AuthorId{get;set;}
}


public class UpdateBlogValidator : AbstractValidator<UpdateBlogCommand>
{
    public UpdateBlogValidator()
    {
        RuleFor(b => b.BlogId)
            .NotEmpty()
            .WithMessage("Blog Id is required");

        RuleFor(b => b.Title)
            .NotEmpty()
            .WithMessage("Blog title is required");

        RuleFor(b => b.Content)
            .NotEmpty()
            .WithMessage("Blog content is required");

        RuleFor(b => b.CategoryId)
            .NotEmpty()
            .WithMessage("Category Id is required");
    }
}

public class UpdateBlogHandler(IRepositoryBase<Blog> _repository, IUnitOfWork _unitOfWork) : IRequestHandler<UpdateBlogCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var blog = await _repository.GetByIdAsync(request.BlogId)!;
        if (blog == null)
        {
            return Errors.NotFound;
        }
        if(request.AuthorId!=blog.AuthorId)
            return Errors.NotAuthorized;
        blog.Title= request.Title;
        blog.Content= request.Content;
        blog.ImageUrl= request.ImageUrl;
        blog.CoverImageUrl= request.CoverImageUrl;
        blog.CategoryId= request.CategoryId;

        _repository.Update(blog);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(blog.Id);
    }
}
