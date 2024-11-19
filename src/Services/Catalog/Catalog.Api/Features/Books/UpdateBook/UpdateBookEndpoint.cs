namespace Catalog.Api.Features.Books.UpdateBook;

public record UpdateBookRequest(
    Guid Id, 
    string Title, 
    string Description, 
    decimal Price, 
    IEnumerable<Guid> AuthorsIds, 
    IEnumerable<Guid> GenresIds);

public record UpdateBookResponse(
    bool Success);

public class UpdateBookEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/books", async (UpdateBookRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateBookCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateBookResponse>();
            return Results.Ok(response);
        });
    }
}
