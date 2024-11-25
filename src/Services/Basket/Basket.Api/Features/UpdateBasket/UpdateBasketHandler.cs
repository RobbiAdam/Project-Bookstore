namespace Basket.Api.Features.UpdateBasket;

public record UpdateBasketCommand(
    string Username, IEnumerable<StoreBasketItemDto> Items) : ICommand<UpdateBasketResult>;

public record UpdateBasketResult(
    bool Success, string Message);


public class UpdateBasketCommandHandler(
    ApplicationDbContext context,
    IBasketService basketService) : ICommandHandler<UpdateBasketCommand, UpdateBasketResult>
{
    public async Task<UpdateBasketResult> Handle(UpdateBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await context.ShoppingCarts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Username == command.Username, cancellationToken);

        if (basket is null)
            return new UpdateBasketResult(false, $"Basket for {command.Username} does not exist");

        var basketItems = await basketService.FetchBasketItemsAsync(command.Items, cancellationToken);

        UpdateBasketItems(basket, basketItems);

        await context.SaveChangesAsync(cancellationToken);
        return new UpdateBasketResult(true, $"Basket for {command.Username} has been updated");
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

