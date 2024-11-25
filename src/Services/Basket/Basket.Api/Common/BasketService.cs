using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.Api.Common;

public class BasketService(IRequestClient<GetBookInfoEventRequest> bookClient) : IBasketService
{
    public async Task<IEnumerable<ShoppingCartItem>> FetchBasketItemsAsync(
    IEnumerable<StoreBasketItemDto> items, CancellationToken cancellationToken)
    {
        var tasks = items.Select(item => FetchBookInfoAsync(item, cancellationToken));
        return await Task.WhenAll(tasks);
    }

    private async Task<ShoppingCartItem> FetchBookInfoAsync(StoreBasketItemDto item, CancellationToken cancellationToken)
    {
        if (item.Quantity <= 0)
            throw new InvalidOperationException("Quantity must be greater than 0");

        var response = await bookClient.GetResponse<GetBookInfoEventResponse>(
            new GetBookInfoEventRequest(BookId: item.BookId), cancellationToken);

        if (response.Message is null)
            throw new InvalidOperationException($"Book with Id {item.BookId} not found");

        return new ShoppingCartItem
        {
            BookId = item.BookId,
            BookTitle = response.Message.Title,
            Price = response.Message.Price,
            Quantity = item.Quantity
        };
    }
}
