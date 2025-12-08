
namespace zenblog.application.Categories.Commands.CreateCategory;

public record class CreateCategoryCommand(string Name) : IRequest<bool> { }
