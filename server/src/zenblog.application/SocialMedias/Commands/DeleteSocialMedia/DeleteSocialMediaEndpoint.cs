namespace zenblog.application.SocialMedias.Commands.DeleteSocialMedia;

public class DeleteSocialMediaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/socialmedias/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteSocialMediaCommand(id));
            if(!result.IsSuccess) return Results.Problem(result.Errors!.First().Description);
            return Results.NoContent();
        })
        .WithName("DeleteSocialMedia")
        .WithTags("SocialMedias")
        .WithDescription("Removes a social media link by its ID.")
        .Produces<Guid>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}
