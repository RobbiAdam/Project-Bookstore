namespace Catalog.Api.Features.Products.CreateProduct;

public record CreateBookRequest(
    string Title, 
    string Description, 
    decimal Price, 
    IEnumerable<Guid> AuthorsIds, 
    IEnumerable<Guid> GenresIds);

public record CreateBookResponse(
    Guid Id);

public class CreateBookEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/books", async (CreateBookRequest request, ISender sender ) =>
        {
            var command = request.Adapt<CreateBookCommand>();
            
            var result = await sender.Send(command);

            var response = result.Adapt<CreateBookResponse>();

            return Results.Created($"/books/{response.Id}", response);
        })
            .WithName("CreateBook")
            .Produces<CreateBookResponse>(201);  
    }
}
