
namespace zenblog.application.Blogs.Commands.DeleteBlog;

public record DeleteBlogRequest(Guid BlogId);
public record DeleteBlogResponse(string Message = "Blog deleted successfully");

public class DeleteBlogEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/blogs/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBlogCommand(id));
            if (!result.IsSuccess)
            {
                return Results.BadRequest(result.Errors);
            }
            return Results.Ok(new DeleteBlogResponse());
        });
    }
}


