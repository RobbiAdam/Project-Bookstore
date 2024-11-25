namespace Basket.Api.Common;

public interface IBasketService
{
    Task<IEnumerable<ShoppingCartItem>> FetchBasketItemsAsync(IEnumerable<StoreBasketItemDto> items, CancellationToken cancellationToken);
}
