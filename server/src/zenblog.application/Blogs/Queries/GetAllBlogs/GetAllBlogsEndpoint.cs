

namespace zenblog.application.Blogs.Queries.GetAllBlogs;


public record GetAllBlogsResponse(List<BlogDto> Blogs);

public class GetAllBlogsEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/blogs", async (ISender sender) =>
        {
            var result =  await sender.Send(new GetAllBlogsQuery());
            return Results.Ok(new GetAllBlogsResponse(result.Data!));
        })
        .WithTags("Blogs")
        .Produces<GetAllBlogsResponse>(StatusCodes.Status200OK)
        .WithName("GetAllBlogs")
        .WithDescription("Gets all blogs");
    }
}