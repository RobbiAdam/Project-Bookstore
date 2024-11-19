namespace Catalog.Api.Features.Genres.CreateGenre;

public record CreateGenreRequest(
    string Name);

public record CreateGenreResponse(
    Guid Id);

public class CreateGenreEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/genres", async(CreateGenreRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateGenreCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateGenreResponse>();

            return Results.Created($"/genres/{response.Id}", response);
        });



    }
}
