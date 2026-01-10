
namespace zenblog.application.ContactInfos.Commands.DeleteContactInfo;

public record DeleteContactInfoResponse(string Message);

public class DeleteContactInfoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/contact-infos/{id:guid}", async(Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteContactInfoCommand(Id));
            if(!result.IsSuccess)
                return Results.BadRequest("An error occured while attempting to delete this item");
            return Results.Ok(new DeleteContactInfoResponse($"Contact Info with {Id} deleted successfully"));
        }).WithName("DeleteContactInfo")
        .WithTags("ContactInfos")
        .Produces<DeleteContactInfoResponse>()
        .RequireAuthorization();
    }
}