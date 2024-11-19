namespace Catalog.Api.Features.Genres.UpdateGenre;

public record UpdateGenreRequest(
    Guid Id, string Name);

public record UpdateGenreResponse(
    bool Success);
public class UpdateGenreEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/genres/", async (UpdateGenreRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateGenreCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateGenreResponse>();
            return Results.Ok(response);
        });
    }
}
