using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Features.Authors.UpdateAuthor;

public record UpdateAuthorRequest(string Name);

public record UpdateAuthorResponse(bool Success);
public class UpdateAuthorEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/authors/{id}", async (Guid id, [FromBody] UpdateAuthorRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateAuthorCommand>();
            command = command with { Id = id };
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateAuthorResponse>();
            return Results.Ok(response);
        });
    }
}
