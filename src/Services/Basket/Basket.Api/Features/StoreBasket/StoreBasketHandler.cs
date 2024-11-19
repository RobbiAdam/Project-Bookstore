namespace Basket.Api.Features.StoreBasket;

public record StoreBasketCommand(string Username, IEnumerable<ShoppingCartItem> Items) : ICommand<StoreBasketResult>;

public record StoreBasketResult(
    string Username);

public class StoreBasketCommandHandler(
    ApplicationDbContext context) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await context.ShoppingCarts.FirstOrDefaultAsync(x => x.Username == command.Username, cancellationToken);

        if (basket != null)
        {
            throw new Exception("Basket already exists");
        }

        basket = new ShoppingCart(
            userName: command.Username
            );

        basket.AddItems(command.Items);

        await context.ShoppingCarts.AddAsync(basket, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new StoreBasketResult(command.Username);
    }
}
