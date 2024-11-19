namespace Basket.Api.Dtos;

public record ShoppingCartDto(
    string Username,
    IEnumerable<ShoppingCartItemDto> Items,
    decimal TotalPrice);

