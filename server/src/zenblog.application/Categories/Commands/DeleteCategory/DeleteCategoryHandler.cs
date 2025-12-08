namespace zenblog.application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid Id): IRequest<bool>;
internal class DeleteCategoryHandler(IRepositoryBase<Category> _repository,IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCategoryCommand, bool>
{
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryToDelete = await _repository.GetByIdAsync(request.Id)!;
        if(categoryToDelete is null) {
            // to do
            return false;
        }
         _repository.Delete(categoryToDelete);

        return await unitOfWork.SaveChangesAsync();
    }
}