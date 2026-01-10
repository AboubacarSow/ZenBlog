namespace zenblog.application.Comments.Queries.GetCommentById;
public record GetCommentByIdResponse(CommentDto Comment);
public class GetCommentByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/comments/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetCommentByIdQuery(id), cancellationToken);
            if(!result.IsSuccess)
            {
                return Results.NotFound(result.Errors!.FirstOrDefault()!.Description);
            }
            return Results.Ok(new GetCommentByIdResponse(result.Data!));
        }).WithName("GetCommentById")
        .WithTags("Comments")
        .WithDescription("Fetches a comment using its unique identifier.")
        .Produces<GetCommentByIdResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .RequireAuthorization();
    }
}