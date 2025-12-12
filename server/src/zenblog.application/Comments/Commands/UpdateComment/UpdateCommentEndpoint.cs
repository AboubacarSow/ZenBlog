namespace zenblog.application.Comments.Commands.UpdateComment;

public record UpdateCommentRequest(UpdateCommentCommand Command);
public record UpdateCommentResponse;

public class UpdateCommentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/comments/{id:guid}", async (Guid id, UpdateCommentRequest request, ISender sender) =>
        {
            if (id != request.Command.Id)
            {
                return Results.BadRequest("ID in URL does not match ID in command.");
            }

            var result = await sender.Send(request.Command);
            if (!result.IsSuccess) return Results.Problem(result.Errors!.First().Description);
            return Results.NoContent();
        }).WithName("UpdateComment")
          .WithTags("Comments")
          .WithDescription("Updates an existing comment by ID.");
    }
}
