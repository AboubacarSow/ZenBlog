namespace zenblog.application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid Id): IRequest<Result<Guid>>;

public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Category ID is required.");
    }
}
internal class DeleteCategoryHandler(IRepositoryBase<Category> _repository,IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCategoryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryToDelete = await _repository.GetByIdAsync(request.Id)!;
        if(categoryToDelete is null) {
            return Errors.NotFound;
        }
         _repository.Delete(categoryToDelete);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken);
        if(!result)
        {
            return Errors.FailedToDelete;
        }
        return Result<Guid>.Success(categoryToDelete.Id);
    }
}
