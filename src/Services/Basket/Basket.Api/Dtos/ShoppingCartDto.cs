namespace Basket.Api.Dtos;

public record ShoppingCartDto(
    string Username,
    IEnumerable<ShoppingCartItemDto> Items,
    decimal TotalPrice);

public record ShoppingCartItemDto(
    Guid BookId,
    string BookTitle,
    int Quantity,
    decimal Price
    );