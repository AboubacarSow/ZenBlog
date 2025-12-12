namespace zenblog.application.ContactInfos.Queries.GetContactInfos;


public record GetContactInfosResponse(List<ContactInfoDto> ContactInfos);
public class GetContactInfosEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/contactinfos", async (ISender sender) =>
        {
            var result = await sender.Send(new GetContactInfosQuery());
            return Results.Ok(new GetContactInfosResponse(result.Data!));
        })
        .WithName("GetContactInfos")
        .Produces<GetContactInfosResponse>(StatusCodes.Status200OK)
        .WithTags("ContactInfos")
        .WithDescription("Retrieves all contact information entries.");
    }
}