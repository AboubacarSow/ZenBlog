namespace zenblog.application.Blogs.Commands.UpdateBlog;


public record UpdateBlogRequest(UpdateBlogCommand Request);
public record UpdateBlogResponse(Guid BlogId);
public class UpdateBlogEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/blogs/{id:guid}", async (Guid id, UpdateBlogRequest request, ISender sender) =>
        {
            if (id != request.Request.BlogId)
            {
                return Results.BadRequest("Blog Id in the URL does not match the Blog Id in the request body.");
            }

            var result = await sender.Send(request.Request);
            if (!result.IsSuccess)
            {
                return Results.BadRequest(result.Errors);
            }
            return Results.Ok(new UpdateBlogResponse(result.Data));
        });
    }
}
