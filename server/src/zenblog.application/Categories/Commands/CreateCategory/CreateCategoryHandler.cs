namespace zenblog.application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name) : IRequest<Result<Guid>>;
public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
    }
}
public class CreateCategoryHandler(IRepositoryBase<Category> _repository, IUnitOfWork _unitOfWork) : IRequestHandler<CreateCategoryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
        };

        await _repository.CreateAsync(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(category.Id);
    }
}

