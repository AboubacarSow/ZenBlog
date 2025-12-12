using zenblog.application.Messages.Dtos;

namespace zenblog.application.Messages.Queries.GetMessageById;

public record GetMessageByIdResponse(MessageDto Message);
public class GetMessageByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/messages/{id:guid}", async (
            Guid id,
            ISender sender) => 
        {
            var result = await sender.Send(new GetMessageByIdQuery(id));

            if (!result.IsSuccess)
                return Results.NotFound();

            return Results.Ok(new GetMessageByIdResponse(result.Data!));
        })
        .WithName("GetMessageById")
        .Produces<GetMessageByIdResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("Messages");
    }
}