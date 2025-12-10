

namespace zenblog.application.Categories.Queries.GetCategoryById;

internal class GetCategoryByIdHandler(IRepositoryBase<Category> _repository, IMapper _mapper) :
    IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto?>>
{
    public async Task<Result<CategoryDto?>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id)!;
        if(category is null) 
            return Errors.NotFound;
        var categoryDto = _mapper.Map<CategoryDto>(category);
        return categoryDto;
    }
}