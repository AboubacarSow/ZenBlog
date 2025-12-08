
namespace zenblog.application.Categories.Commands.CreateCategory;
internal class CreateCategoryHandler(IRepositoryBase<Category> _repository,IUnitOfWork unitOfWork, IMapper _mapper)
    : IRequestHandler<CreateCategoryCommand, bool>
{
    public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category= _mapper.Map<Category>(request);
        await _repository.CreateAsync(category);

        return await unitOfWork.SaveChangesAsync();
    }
}