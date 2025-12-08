
namespace zenblog.application.Categories.Queries.GetCategoryById;

internal class GetCategoryByIdHandler(IRepositoryBase<Category> _repository, IMapper _mapper) :
    IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
{
    public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id)!;
        if(category is null)
        {
            //to do
        }
        return _mapper.Map<CategoryDto>(category);
    }
}