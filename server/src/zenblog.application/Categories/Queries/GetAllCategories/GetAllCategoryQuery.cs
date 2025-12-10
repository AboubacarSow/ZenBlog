

namespace zenblog.application.Categories.Queries.GetAllCategories;

public record GetAllCategoryQuery : IRequest<Result<List<CategoryDto>>>;
