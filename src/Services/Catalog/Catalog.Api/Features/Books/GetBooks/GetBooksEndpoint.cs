namespace Catalog.Api.Features.Books.GetBooks;

public record GetBooksResponse(
    IEnumerable<BookDto> Books);

public class GetBooksEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/books", async (ISender sender) =>
        {
            var result = await sender.Send(new GetBooksQuery());
            var response = result.Adapt<GetBooksResponse>();
            return Results.Ok(response);
        });
    }
}
