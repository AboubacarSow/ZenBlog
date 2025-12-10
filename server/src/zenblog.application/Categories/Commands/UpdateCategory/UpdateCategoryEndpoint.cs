namespace zenblog.application.Categories.Commands.UpdateCategory;

public record UpdateCategoryResponse(bool IsSuccess, string? Message);
public record UpdateCategoryRequest(Guid Id, string Name);
public class UpdateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/categories/{id:guid}", async (ISender sender, Guid id, UpdateCategoryRequest request) =>
        {
            if (id != request.Id)
            {
                return Results.BadRequest(new UpdateCategoryResponse(false, "Category ID mismatch."));
            }

            var response = await sender.Send(new UpdateCategoryCommand(request.Id, request.Name));
            if(!response.IsSuccess)
            {
                return Results.BadRequest(new UpdateCategoryResponse(false, response.Errors?.FirstOrDefault()?.Description! ));
            }
            return Results.NoContent();
        })
        .WithName("UpdateCategory")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithTags("Categories")
        .WithDescription("Updates an existing category by its unique identifier.");
    }
}