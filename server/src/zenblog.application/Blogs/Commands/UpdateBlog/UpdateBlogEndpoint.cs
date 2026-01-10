using System.Security.Claims;

namespace zenblog.application.Blogs.Commands.UpdateBlog;


public record UpdateBlogRequest(UpdateBlogCommand Blog);
public record UpdateBlogResponse(Guid BlogId);
public class UpdateBlogEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/blogs/{id:guid}", async (Guid id, UpdateBlogRequest request, ISender sender,ClaimsPrincipal user) =>
        {
            if (id != request.Blog.BlogId)
                return Results.BadRequest("Blog Id in the URL does not match the Blog Id in the request body.");
            request.Blog.AuthorId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await sender.Send(request.Blog);
            if (!result.IsSuccess)
            {
                if(result.Errors?.Any(e => e.Id == "NotAuthorized") == true)
                    return Results.Unauthorized();
                return Results.BadRequest(result.Errors);
            }

            return Results.Ok(new UpdateBlogResponse(result.Data));
        }).WithName("UpdateBlog")
        .WithTags("Blogs")
        .WithDescription("Updates the title, content, or other properties of an existing blog identified by its ID.")
        .Produces<UpdateBlogResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)   // ID mismatch or validation errors
        .Produces(StatusCodes.Status404NotFound)     // Blog not found (if your handler returns NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .RequireAuthorization();
    }
}
