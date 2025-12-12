namespace zenblog.application.Comments.Commands.CreateComment;


public record CreateCommentRequest(CreateCommentCommand Command);
public record CreateCommentResponse(Guid CommentId);
public class CreateCommentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/comments", async(CreateCommentRequest request, ISender sender) =>
        {
             var result = await sender.Send(request.Command);
            if(!result.IsSuccess) return Results.Problem(result.Errors!.First().Description);
            return Results.CreatedAtRoute("GetCommentById", new { CommentId = result.Data }, null);
        }).WithName("CreateComment")
          .WithTags("Comments")
          .WithDescription("Creates a new comment for a blog post.");
        
    }
}
