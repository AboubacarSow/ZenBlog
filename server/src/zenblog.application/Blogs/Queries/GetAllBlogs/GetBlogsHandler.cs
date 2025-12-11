

namespace zenblog.application.Blogs.Queries.GetAllBlogs;

public record GetAllBlogsQuery: IRequest<Result<List<BlogDto>>>;
internal class GetAllBlogs(IRepositoryBase<Blog> _repository, IMapper _mapper) : IRequestHandler<GetAllBlogsQuery, Result<List<BlogDto>>>
{
    public async Task<Result<List<BlogDto>>> Handle(GetAllBlogsQuery request, CancellationToken cancellationToken)
    {
        var blogs = await _repository.GetAllAsync(false);
        var blogDtos = _mapper.Map<List<BlogDto>>(blogs);
        return Result<List<BlogDto>>.Success(blogDtos);
    }
}
