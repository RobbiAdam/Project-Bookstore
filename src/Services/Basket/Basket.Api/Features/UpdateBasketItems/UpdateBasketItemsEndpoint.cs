using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Features.UpdateBasket;

public record UpdateBasketItemsRequest(
    IEnumerable<StoreBasketItemDto> Items);
public record UpdateBasketItemsResponse(
    bool Success, string Message);

public class UpdateBasketItemsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/baskets/{username}", async (string username, [FromBody] UpdateBasketItemsRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateBasketItemsCommand>();
            command = command with { Username = username };

            var result = await sender.Send(command);
            var response = result.Adapt<UpdateBasketItemsResponse>();
            return Results.Ok(response);
        });
    }
}
