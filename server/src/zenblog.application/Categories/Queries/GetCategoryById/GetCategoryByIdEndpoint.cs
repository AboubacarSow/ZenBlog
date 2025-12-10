namespace zenblog.application.Categories.Queries.GetCategoryById;

public record GetCategoryByIdResponse(Result<CategoryDto?> Category);

public class GetCategoryByIdEnpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/categories/{id:guid}", async (ISender sender, Guid id) =>
        {
            var response = await sender.Send(new GetCategoryByIdQuery(id));
            return Results.Ok(new GetCategoryByIdResponse(response));

        }).WithName("GetCategoryById")
        .Produces<GetCategoryByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithTags("Categories")
        .WithDescription("Gets a category by its unique identifier.");
    }
}