namespace Catalog.Api.Features.Books.UpdateBook;

public record UpdateBookRequest(
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
        app.MapPut("/books/{id}", async (Guid id, UpdateBookRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateBookCommand>();
            command= command with { Id = id };
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateBookResponse>();
            return Results.Ok(response);
        });
    }
}
