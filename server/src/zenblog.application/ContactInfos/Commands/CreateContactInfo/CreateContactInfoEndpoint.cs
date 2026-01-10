namespace zenblog.application.ContactInfos.Commands.CreateContactInfo;

public record CreCreateContactInfoRequest(CreateContactInfoCommand ContactInfo);
public class CreateContactInfoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/contact-infos", async (
            CreCreateContactInfoRequest request,
            ISender sender) =>
        {
            var result = await sender.Send(request.ContactInfo);

            return result.IsSuccess
                ? Results.Created($"/api/contact-info/{result.Data}", result)
                : Results.BadRequest(result);
        })
        .WithName("CreateContactInfo")
        .WithTags("ContactInfos")
        .Produces<Result<Guid>>(StatusCodes.Status201Created)
        .Produces<Result>(StatusCodes.Status400BadRequest)
        .WithDescription("Creates ContactInfo")
        .RequireAuthorization();
    }
}
