namespace zenblog.application.ContactInfos.Queries.GetContactInfoById;
public record GetContactInfoByResponse(ContactInfoDto ContactInfo);
public class GetContactInfoByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/contact-infos/{id:guid}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new GetContactInfoByIdQuery(id));
            if (!result.IsSuccess)
            {
                return Results.NotFound();
            }
            return Results.Ok(result.Data);
        })
        .WithName("GetContactInfoById")
        .WithTags("ContactInfos")
        .Produces<ContactInfoDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithDescription("");
    }
}