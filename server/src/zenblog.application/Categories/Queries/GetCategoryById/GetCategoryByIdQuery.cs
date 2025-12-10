

namespace zenblog.application.Categories.Queries.GetCategoryById;
public record GetCategoryByIdQuery(Guid Id) : IRequest<Result<CategoryDto?>>;
