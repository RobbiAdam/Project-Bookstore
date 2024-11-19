namespace Basket.Api.Features.DeleteBasket;

public record DeleteBasketRequest(string Username);
public record DeleteBasketResponse(bool Success);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/baskets/{username}", async (string username, ISender sender) =>
        {
            var command = new DeleteBasketCommand(username);
            var result = await sender.Send(command);
            var response = result.Adapt<DeleteBasketResponse>();
            return Results.Ok(response);
        });
    }
}
