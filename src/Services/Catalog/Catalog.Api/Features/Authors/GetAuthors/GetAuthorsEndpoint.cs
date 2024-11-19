namespace Catalog.Api.Features.Authors.GetAuthors;

public record GetAuthorsResponse(IEnumerable<Author> Authors);
public class GetAuthorsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/authors", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAuthorsQuery());
            var response = result.Adapt<GetAuthorsResponse>();
            return Results.Ok(response);
        });
    }
}
