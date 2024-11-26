namespace Basket.Api.Features.DeleteItemsFromBasket;

public record DeleteItemsFromBasketCommand(string Username, IEnumerable<Guid> BookIds) : ICommand<DeleteItemsFromBasketResult>;
public record DeleteItemsFromBasketResult(bool Success);

public class DeleteItemsFromBasketCommandHandler(
    ApplicationDbContext context) : ICommandHandler<DeleteItemsFromBasketCommand, DeleteItemsFromBasketResult>
{
    public async Task<DeleteItemsFromBasketResult> Handle(DeleteItemsFromBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await context.ShoppingCarts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Username == command.Username);

        if (basket is null)
            return new DeleteItemsFromBasketResult(false);

        var bookIdsToRemove = command.BookIds.ToHashSet();

        var itemsToRemove = basket.Items
            .Where(item => bookIdsToRemove.Contains(item.BookId))
            .ToList();

        if(itemsToRemove.Count == 0)
            return new DeleteItemsFromBasketResult(false);


        foreach (var item in itemsToRemove)
        {
            basket.Items.Remove(item);
        }  

        await context.SaveChangesAsync(cancellationToken);
        return new DeleteItemsFromBasketResult(true);
    }
}
