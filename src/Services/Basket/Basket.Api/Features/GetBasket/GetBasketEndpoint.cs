using Basket.Api.Dtos;

namespace Basket.Api.Features.GetBasket;

public record GetBasketResponse(ShoppingCartDto Basket);
public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/baskets/{username}", async (string username, ISender sender) =>
        {
            var query = new GetBasketQuery(username);
            var result = await sender.Send(query);
            var response = result.Adapt<GetBasketResponse>();
            return Results.Ok(response);
        });
    }
}
