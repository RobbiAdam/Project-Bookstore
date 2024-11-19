namespace Catalog.Api.Features.Books.DeleteBook;

public record DeleteBookResponse(bool Success);

public class DeleteBookEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/books/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBookCommand(id));
            var response = result.Adapt<DeleteBookResponse>();
            return Results.Ok(response);
        });
    }
}
