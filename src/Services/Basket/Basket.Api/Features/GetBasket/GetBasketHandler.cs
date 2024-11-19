using Basket.Api.Dtos;

namespace Basket.Api.Features.GetBasket;

public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCartDto Basket);

public class GetBasketQueryHandler(
    ApplicationDbContext context) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await context.ShoppingCarts
            .Include(x => x.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Username == query.Username, cancellationToken);

        if (basket is null)
        {
            throw new Exception("Basket with username " + query.Username + " not found");
        }

        var basketDto = basket.Adapt<ShoppingCartDto>();
        return new GetBasketResult(basketDto);
    }
}
