namespace zenblog.application.Categories.Commands.UpdateCatetory;

public record UpdateCategoryCommand(int Id, string Name): IRequest<bool>;
