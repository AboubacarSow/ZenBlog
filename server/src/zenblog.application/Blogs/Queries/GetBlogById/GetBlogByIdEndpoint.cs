namespace zenblog.application.Blogs.Queries.GetBlogById;


public record GetBlogByIdResponse(BlogDto Blog);
public class GetBlogByIdEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/blogs/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetBlogByIdQuery(id));
            if (!result.IsSuccess)
            {
                return Results.NotFound();
            }
            return Results.Ok(result.Data);
        })
        .WithTags("Blogs")
        .Produces<BlogDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithName("GetBlogById")
        .WithDescription("Gets a blog by Id");
    }
}