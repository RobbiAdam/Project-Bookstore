namespace Basket.Api.Features.UpdateBasket;

public record UpdateBasketItemsCommand(
    string Username, IEnumerable<StoreBasketItemDto> Items) : ICommand<UpdateBasketItemsResult>;

public record UpdateBasketItemsResult(
    bool Success, string Message);

public class UpdateBasketItemsCommandValidator : AbstractValidator<UpdateBasketItemsCommand>
{
    public UpdateBasketItemsCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Items).NotEmpty().WithMessage("Items are required");
    }
}
public class UpdateBasketItemsCommandHandler(
    ApplicationDbContext context,
    IBasketService basketService) : ICommandHandler<UpdateBasketItemsCommand, UpdateBasketItemsResult>
{
    public async Task<UpdateBasketItemsResult> Handle(UpdateBasketItemsCommand command, CancellationToken cancellationToken)
    {
        var basket = await context.ShoppingCarts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Username == command.Username, cancellationToken);

        if (basket is null)
            return new UpdateBasketItemsResult(false, $"Basket for {command.Username} does not exist");

        var basketItems = await basketService.FetchBasketItemsAsync(command.Items, cancellationToken);

        UpdateBasketItems(basket, basketItems);

        await context.SaveChangesAsync(cancellationToken);
        return new UpdateBasketItemsResult(true, $"Basket for {command.Username} has been updated");
    }

    private void UpdateBasketItems(ShoppingCart basket, IEnumerable<ShoppingCartItem> basketItems)
    {
        var existingItemDict = basket.Items.ToDictionary(x => x.BookId);

        foreach (var item in basketItems)
        {
            if (existingItemDict.TryGetValue(item.BookId, out var existingItem))
            {
                existingItem.Quantity += item.Quantity;
                existingItem.BookTitle = item.BookTitle;
                existingItem.Price = item.Price;
            }
            else
            {
                basket.Items.Add(item);
            }
        }
    }
}

