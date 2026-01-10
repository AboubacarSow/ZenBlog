using System.Security.Claims;

namespace zenblog.application.Blogs.Commands.DeleteBlog;

public record DeleteBlogRequest(Guid BlogId);
public record DeleteBlogResponse(string Message = "Blog deleted successfully");

public class DeleteBlogEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/blogs/{id:guid}", async (Guid id, ISender sender, ClaimsPrincipal user) =>
        {
            var authorId= Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await sender.Send(new DeleteBlogCommand(id,authorId));
            if (!result.IsSuccess)
            {
                if(result.Errors?.Any(e => e.Id == "NotAuthorized") == true)
                {
                    return Results.Unauthorized();
                }
                return Results.BadRequest(result.Errors);
            }
            return Results.Ok(new DeleteBlogResponse());
        }).WithName("DeleteBlog")
        .WithTags("Blogs")
        .WithDescription("Removes the specified blog from the system using its unique identifier.")
        .Produces<DeleteBlogResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .RequireAuthorization();
    }
}


