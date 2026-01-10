using zenblog.application.Messages.Dtos;

namespace zenblog.application.Messages.Queries.GetAllMessages;

public record GetAllMessagesResponse(List<MessageDto>? Messages);
public class GetAllMessagesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/messages", async(ISender sender) =>
        {
            var result = await sender.Send(new GetMessagesQuery());
            var response= new GetAllMessagesResponse(result.Data!);
            return Results.Ok(response);
        }).WithName("GetMessages")
        .WithTags("Messages")
        .Produces<GetAllMessagesResponse>()
        .RequireAuthorization();
    }
}