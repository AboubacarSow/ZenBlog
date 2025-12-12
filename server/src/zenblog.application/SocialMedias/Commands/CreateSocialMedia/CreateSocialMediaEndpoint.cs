namespace zenblog.application.SocialMedias.Commands.CreateSocialMedia;

public class CreateSocialMediaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/socialmedias", async (CreateSocialMediaCommand cmd, ISender sender) =>
        {
            var result = await sender.Send(cmd);
            if(!result.IsSuccess) return Results.BadRequest(result);
            return Results.Created($"/socialmedias/{result.Data}", result.Data);
        })
        .WithName("CreateSocialMedia")
        .WithTags("SocialMedia")
        .WithDescription("Adds a new social media link to the system.")
        .Produces<Guid>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}


