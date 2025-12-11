namespace zenblog.application.Blogs.Queries.GetBlogById;

public record GetBlogByIdQuery(Guid Id): IRequest<Result<BlogDto>>;

public class GetBlogByIdValidator : AbstractValidator<GetBlogByIdQuery>
{
    public GetBlogByIdValidator()
    {
        RuleFor(u=>u.Id)
            .NotEmpty()
            .WithMessage("Blog Id is required for this request");
    }
}

public class GetBlogByIdHandler(IRepositoryBase<Blog> _repository, IMapper _mapper) : IRequestHandler<GetBlogByIdQuery, Result<BlogDto>>
{
    public async Task<Result<BlogDto>> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
    {
        var blog = await _repository.GetByIdAsync(request.Id)!;
        if (blog == null)
        {
            return Errors.NotFound;
        }
        var blogDto = _mapper.Map<BlogDto>(blog);
        return Result<BlogDto>.Success(blogDto);
    }
}
