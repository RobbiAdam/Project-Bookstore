namespace Basket.Api.Dtos;

public record StoreBasketItemDto(
    Guid BookId,
    int Quantity
    );