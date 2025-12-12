namespace zenblog.application.SocialMedias.Queries.GetSocialMedias;

public record GetSocialMediasResponse(IEnumerable<SocialMediaDto>? SocialMedias);
public class GetSocialMediasEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/socialmedias", async (ISender sender) =>
        {
            var result = await sender.Send(new GetSocialMediasQuery());
            return Results.Ok(new GetSocialMediasResponse(result.Data));
        })
        .WithName("GetSocialMedias")
        .WithTags("SocialMedia")
        .WithDescription("Returns a list of all available social media entries.")
        .Produces<IEnumerable<SocialMediaDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}

