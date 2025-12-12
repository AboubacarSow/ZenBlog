namespace zenblog.application.Comments.Commands.DeleteComment;

public record DeleteCommentResponse(string Message = "Comment deleted successfully.");
public class DeleteCommentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/comments/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteCommentCommand(id));
            if (!result.IsSuccess) return Results.Problem(result.Errors!.First().Description);
            return Results.Ok(new DeleteCommentResponse());
        }).WithName("DeleteComment")
          .Produces<DeleteCommentResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status404NotFound)
          .WithTags("Comments")
          .WithDescription("Deletes an existing comment by ID.");
    }
}