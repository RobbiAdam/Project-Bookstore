namespace Catalog.Api.Features.Genres.DeleteGenre;

public record DeleteGenreResponse(bool Success);

public class DeleteGenreEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/genres/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteGenreCommand(id));
            var response = result.Adapt<DeleteGenreResponse>();
            return Results.Ok(response);
        });
    }
}
