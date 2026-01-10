using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace zenblog.application.Blogs.Commands.CreateBlog;


public record CreateBlogResponse(Guid BlogId);
public record CreateBlogRequest(
    CreateBlogCommand Blog
);
public class CreateBlogEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/blogs", async ([FromBody]CreateBlogRequest request, ISender sender, ClaimsPrincipal user) =>
        {
            var command = request.Blog;
            command.AuthorId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
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
        .WithDescription("Creates a new blog")
        .RequireAuthorization();
    }
}