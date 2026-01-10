namespace zenblog.application.Categories.Commands.CreateCategory;


public record CreateCategoryRequest(string Name) : IRequest<Result<Guid>>;
public record CreateCategoryResponse(string Endpoint, object RouteValues);
public class CreateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/categories", async (ISender sender, CreateCategoryRequest request) =>
        {
            var response = await sender.Send(new CreateCategoryCommand(request.Name));
            return Results.CreatedAtRoute("GetCategoryById", new { id = response.Data });
        })
        .WithName("CreateCategory")
        .Produces(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithTags("Categories")
        .WithDescription("Creates a new category.")
        .RequireAuthorization();
    }
}