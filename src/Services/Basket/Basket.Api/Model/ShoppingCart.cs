namespace Basket.Api.Model;

public class ShoppingCart
{
    public string Username { get; private set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public ShoppingCart(string userName)
    {
        Username = userName;
    }

    private ShoppingCart() { }


    public void AddItems(IEnumerable<ShoppingCartItem> items)
    {
        foreach (var item in items)
        {
            Items.Add(item);
        }
    }
}
