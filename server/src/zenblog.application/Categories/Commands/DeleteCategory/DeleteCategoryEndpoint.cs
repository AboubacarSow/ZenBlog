using Microsoft.AspNetCore.Mvc;

namespace zenblog.application.Categories.Commands.DeleteCategory;


public record DeleteCategoryRequest(Guid Id);
public record DeleteCategoryResponse(bool IsSuccess, string? Message);
  
public class DeleteCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/categories/{id:guid}", async ([FromRoute]Guid id, IMediator mediator) =>
        {
            var command = new DeleteCategoryCommand(id);
            var result = await mediator.Send(command);
            return result.IsSuccess ? Results.Ok(new DeleteCategoryResponse(true,$"Category with Id:{result.Data} deleted successfully.")) : Results.BadRequest(result.Errors);
        }).WithName("DeleteCategory")
          .Produces<bool>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound)
          .WithTags("Categories")
          .WithDescription("Deletes a category by its unique identifier.");
    }
}