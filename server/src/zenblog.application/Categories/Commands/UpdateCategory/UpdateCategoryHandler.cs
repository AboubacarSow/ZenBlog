namespace zenblog.application.Categories.Commands.UpdateCategory;
public record UpdateCategoryCommand(Guid Id, string Name): IRequest<Result<bool>>;
public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Category ID is required.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
    }
}

internal class UpdateCategoryHandler(IRepositoryBase<Category> _repository,IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCategoryCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category= new Category { Id = request.Id, Name = request.Name };
         _repository.Update(category);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken);
        if(!result)
        {
            return Errors.FailedToUpdate;
        }
        return Result<bool>.Success(true);
    }
}