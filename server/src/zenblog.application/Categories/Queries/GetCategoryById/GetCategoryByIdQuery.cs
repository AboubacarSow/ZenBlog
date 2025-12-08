
namespace zenblog.application.Categories.Queries.GetCategoryById;
public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto?>;
