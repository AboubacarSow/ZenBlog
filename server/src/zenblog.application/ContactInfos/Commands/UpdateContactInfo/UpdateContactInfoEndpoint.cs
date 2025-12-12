
namespace zenblog.application.ContactInfos.Commands.UpdateContactInfo;


public record UpdateContactInfoRequest(UpdateContactInfoCommand ContactInfo);
public class UpdateContactInfoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/contact-infos",async (UpdateContactInfoRequest request, ISender sender) => {
            
            var result = await sender.Send(request.ContactInfo);
            if(!result.IsSuccess) return Results.BadRequest(result.Errors!.First().Description);
            return Results.NoContent();
        })
        .WithName("UpdateContactInfo")
        .WithTags("ContactInfos")
        .WithDescription("Edits contact info");
    }
}