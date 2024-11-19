namespace Basket.Api.Model;

public class ShoppingCartItem
{
    public Guid BookId { get; set; }
    public string BookTitle { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}
