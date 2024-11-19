namespace Catalog.Api.Features.Books.GetBook;
public record GetBookByIdResponse(
    BookDto Book);
public class GetBookByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/books/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetBookByIdQuery(id);

            var result = await sender.Send(query);

            var response = result.Adapt<GetBookByIdResponse>();

            return Results.Ok(response);
        });
    }
}
