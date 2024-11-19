namespace Basket.Api.Dtos;

public record ShoppingCartItemDto(
    Guid BookId,
    string BookTitle,
    int Quantity,
    decimal Price
    );