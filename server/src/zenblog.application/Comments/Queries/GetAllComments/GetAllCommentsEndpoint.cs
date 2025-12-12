
namespace zenblog.application.Comments.Queries.GetAllComments;


public record GetCommentsResponse(List<CommentDto> Comments);
public class GetAllCommentsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/comments", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAllCommentsQuery());
            if (!result.IsSuccess)
            {
                return Results.BadRequest(result.Errors);
            }
            return Results.Ok(new GetCommentsResponse(result.Data!));
        })
        .WithName("GetAllComments")
        .WithTags("Comments")
        .WithDescription("Returns a collection of all comments stored in the system.")
        .Produces<GetCommentsResponse>(StatusCodes.Status200OK)
        .Produces<IEnumerable<Error>>(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}