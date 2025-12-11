namespace zenblog.application.Blogs.Commands.CreateBlog;


public record CreateBlogResponse(Guid BlogId);
public record CreateBlogRequest(
    CreateBlogCommand Request
);
public class CreateBlogEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/blogs", async (CreateBlogRequest request, ISender sender) =>
        {
            var command = request.Request;
            var result = await sender.Send(command);
            if (!result.IsSuccess)
            {
                return Results.BadRequest(result.Errors);
            }
            return Results.Created($"/api/blogs/{result.Data}", result.Data);
        })
        .WithTags("Blogs")
        .Produces<Guid>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("CreateBlog")
        .WithDescription("Creates a new blog");
    }
}