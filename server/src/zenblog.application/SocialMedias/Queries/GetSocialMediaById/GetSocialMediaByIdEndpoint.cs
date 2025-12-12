
namespace zenblog.application.SocialMedias.Queries.GetSocialMediaById;

public record GetSocialMediaByIdResponse(SocialMediaDto? SocialMedia);
public class GetSocialMediaByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/socialmedias/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetSocialMediaByIdQuery(id));
            if(!result.IsSuccess) return Results.NotFound();
            return Results.Ok(new GetSocialMediaByIdResponse(result.Data));
        }).WithName("GetSocialMediaById")
        .WithTags("SocialMedia")
        .WithSummary("Get a social media entry by ID")
        .WithDescription("Retrieves a specific social media link using its unique identifier.")
        .Produces<GetSocialMediaByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound);
        
    }
}


