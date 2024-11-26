namespace Basket.Api.Features.StoreBasket;

public record StoreBasketCommand(
    string Username, IEnumerable<StoreBasketItemDto> Items) : ICommand<StoreBasketResult>;

public record StoreBasketResult(
    string Username);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username must not be empty");
        RuleFor(x => x.Items).NotEmpty().WithMessage("Items must not be empty");
    }
}

public class StoreBasketCommandHandler(
    ApplicationDbContext context,
    IBasketService basketService) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        if (!command.Items.Any())
            throw new InvalidOperationException("Items must not be empty");

        var basket = await CreateBasketAsync(command.Username, cancellationToken);
        var basketItems = await basketService.FetchBasketItemsAsync(command.Items, cancellationToken);

        basket.AddItems(basketItems);

        await context.SaveChangesAsync(cancellationToken);
        return new StoreBasketResult(command.Username);
    }

    private async Task<ShoppingCart> CreateBasketAsync(string username, CancellationToken cancellationToken)
    {
        var basket = await context.ShoppingCarts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Username == username, cancellationToken);

        if (basket != null)
        {
            basket.Items.Clear();
            return basket;
        }

        var newBasket = new ShoppingCart(username);
        await context.ShoppingCarts.AddAsync(newBasket, cancellationToken);
        return newBasket;
    }


}
