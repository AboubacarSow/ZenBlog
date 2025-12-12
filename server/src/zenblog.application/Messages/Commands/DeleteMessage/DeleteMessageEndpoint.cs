namespace zenblog.application.Messages.Commands.DeleteMessage;

public class DeleteMessageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/messages/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteMessageCommand(id));

            if(!result.IsSuccess) return Results.Problem($"Failed to delete Message with ID:{id}");
            return Results.NoContent();
        })
        .WithName("DeleteMessage")
        .WithSummary("Delete a message by its Id")
        .WithDescription("Deletes a message and returns its Id.")
        .Produces<Guid>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}

