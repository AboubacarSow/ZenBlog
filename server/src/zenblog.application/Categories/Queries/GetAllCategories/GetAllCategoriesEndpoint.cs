


namespace zenblog.application.Categories.Queries.GetAllCategories;
public record GetAllCategoriesResponse(List<CategoryDto> Categories);
public class GetAllCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/categories", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAllCategoryQuery());
            return Results.Ok(new GetAllCategoriesResponse(result.Data!));
        })
        .WithName("GetAllCategories")
        .Produces<GetAllCategoriesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithTags("Categories")
        .WithDescription("Gets all categories.");
    }
}