namespace Catalog.Api.Features.Genres.UpdateGenre;

public record UpdateGenreRequest(
    string Name);

public record UpdateGenreResponse(
    bool Success);
public class UpdateGenreEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/genres/{id}", async (Guid id, UpdateGenreRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateGenreCommand>();
            command = command with { Id = id };
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateGenreResponse>();
            return Results.Ok(response);
        });
    }
}
