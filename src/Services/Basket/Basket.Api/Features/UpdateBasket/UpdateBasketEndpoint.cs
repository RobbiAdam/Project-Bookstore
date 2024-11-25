namespace Basket.Api.Features.UpdateBasket;

public record UpdateBasketRequest(
    string Username,IEnumerable<StoreBasketItemDto> Items);
public record UpdateBasketResponse(
    bool Success, string Message);

public class UpdateBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/baskets/", async (UpdateBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateBasketCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateBasketResponse>();
            return Results.Ok(response);
        });
    }
}
