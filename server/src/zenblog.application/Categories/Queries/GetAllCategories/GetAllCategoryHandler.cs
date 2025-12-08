
namespace zenblog.application.Categories.Queries.GetAllCategories;
internal class GetAllCategoryHandler(IRepositoryBase<Category> repository, IMapper _mapper) : IRequestHandler<GetAllCategoryQuery, List<CategoryDto>>
{
    public async Task<List<CategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await repository.GetAllAsync();
        return _mapper.Map<List<CategoryDto>>(categories);
    }
}
