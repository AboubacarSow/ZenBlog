namespace zenblog.application.Messages.Commands.CreateMessage;

public class CreateMessageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/messages", async (CreateMessageCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            if(!result.IsSuccess) return Results.BadRequest( result);
            return Results.CreatedAtRoute($"api/messages/{result.Data}",result.Data);
        })
        .WithName("CreateMessage")
        .WithTags("Messages")
        .WithDescription("Creates a new message and returns the generated Id.")
        .Produces<Guid>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
