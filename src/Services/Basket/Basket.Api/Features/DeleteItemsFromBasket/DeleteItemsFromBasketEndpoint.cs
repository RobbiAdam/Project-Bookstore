using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Features.DeleteItemsFromBasket;

public record DeleteItemsFromBasketRequest(
    IEnumerable<Guid> BookIds);

public record DeleteItemsFromBasketResponse(
    bool Success);
public class DeleteItemsFromBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/baskets/{username}/items", async (string username,[FromBody] DeleteItemsFromBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<DeleteItemsFromBasketCommand>();
            command = command with { Username = username };

            var result = await sender.Send(command);
            var response = result.Adapt<DeleteItemsFromBasketResponse>();
            return Results.Ok(response);
        });
    }
}
