namespace Catalog.Api.Features.Genres.GetGenres;

public record GetGenresResponse(IEnumerable<Genre> Genres);
public class GetGenresEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/genres", async (ISender sender) =>
        {
            var result = await sender.Send(new GetGenresQuery());
            var response = result.Adapt<GetGenresResponse>();
            return Results.Ok(response);
        });
    }
}
