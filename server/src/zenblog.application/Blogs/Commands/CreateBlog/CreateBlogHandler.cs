namespace zenblog.application.Blogs.Commands.CreateBlog;


public record CreateBlogCommand(
    string Title,
    string Content,
    string ImageUrl,
    string CoverImageUrl,
    Guid CategoryId
) : IRequest<Result<Guid>>
{
    public Guid? AuthorId{get;set;}
}

public class CreateBlogValidator : AbstractValidator<CreateBlogCommand>
{
    public CreateBlogValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty()
            .WithMessage("Blog title is required");

        RuleFor(b => b.Content)
            .NotEmpty()
            .WithMessage("Blog content is required");

        RuleFor(b => b.CategoryId)
            .NotEmpty()
            .WithMessage("Category Id is required");
        RuleFor(b => b.AuthorId)
            .NotEmpty()
            .WithMessage("AuthorId is required");
    }
}

public class CreateBlogHandler(IRepositoryBase<Blog> _repository, IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<CreateBlogCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var blog = _mapper.Map<Blog>(request);
        await _repository.CreateAsync(blog);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(blog.Id);
    }
}
