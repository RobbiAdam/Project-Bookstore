namespace Catalog.Api.Features.Authors.CreateAuthor;

public record CreateAuthorRequest(
    string Name);

public record CreateAuthorResponse(
    Guid Id);

public class CreateAuthorEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/authors", async (CreateAuthorRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateAuthorCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateAuthorResponse>();
            return Results.Created($"/authors/{response.Id}", response);
        });
    }
}
