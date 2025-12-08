namespace zenblog.application.Categories.Commands.UpdateCatetory;
internal class UpdateCategoryHandler(IRepositoryBase<Category> _repository,IUnitOfWork unitOfWork, IMapper _mapper)
    : IRequestHandler<UpdateCategoryCommand, bool>
{
    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category= _mapper.Map<Category>(request);
         _repository.Update(category);

        return await unitOfWork.SaveChangesAsync();
    }
}