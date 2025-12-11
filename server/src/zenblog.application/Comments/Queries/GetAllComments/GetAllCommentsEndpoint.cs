
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
        });
    }
}