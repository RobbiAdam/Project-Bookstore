namespace Catalog.Api.Features.Authors.UpdateAuthor;

public record UpdateAuthorRequest(Guid Id, string Name);

public record UpdateAuthorResponse(bool Success);
public class UpdateAuthorEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/authors/", async (UpdateAuthorRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateAuthorCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateAuthorResponse>();
            return Results.Ok(response);
        });
    }
}
