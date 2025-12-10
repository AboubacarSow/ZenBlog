namespace zenblog.application.Categories.Queries.GetAllCategories;
internal class GetAllCategoryHandler(IRepositoryBase<Category> repository, IMapper _mapper) : IRequestHandler<GetAllCategoryQuery, Result<List<CategoryDto>>>
{
    public async Task<Result<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await repository.GetAllAsync();
        var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
        return categoryDtos;
    }
}
