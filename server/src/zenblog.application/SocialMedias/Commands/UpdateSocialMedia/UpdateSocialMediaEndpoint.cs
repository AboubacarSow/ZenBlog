namespace zenblog.application.SocialMedias.Commands.UpdateSocialMedia;

public record UpdateSocialMediaRequest(SocialMediaDto SocialMedia);
public class UpdateSocialMediaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/socialmedias/{id:guid}", async (Guid id, UpdateSocialMediaRequest request, ISender sender) =>
        {
            if(id != request.SocialMedia.Id) return Results.BadRequest("Social Media Id mismatche");
            var command = new UpdateSocialMediaCommand(request.SocialMedia);
            var result = await sender.Send(command);
            if(!result.IsSuccess) return Results.Problem(result.Errors!.First().Description);
            return Results.NoContent();
        })
        .WithName("UpdateSocialMedia")
        .WithTags("SocialMedia")
        .WithSummary("Update a social media entry")
        .WithDescription("Updates a specific social media link by its ID.")
        .Produces<Guid>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
