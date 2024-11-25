namespace Basket.Api.Features.StoreBasket;

public record StoreBasketRequest(string Username, IEnumerable<StoreBasketItemDto> Items);
public record StoreBasketResponse(string Username);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/baskets", async (StoreBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreBasketCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<StoreBasketResponse>();
            return Results.Created($"/baskets/{response.Username}", response);
        });
    }
}
