namespace Catalog.Api.Features.Authors.DeleteAuthor;

public record DeleteAuthorResponse(bool Success);

public class DeleteAuthorEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/authors/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteAuthorCommand(id));
            var response = result.Adapt<DeleteAuthorResponse>();
            return Results.Ok(response);
        });
    }
}
