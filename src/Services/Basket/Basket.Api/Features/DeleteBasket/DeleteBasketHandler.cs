namespace Basket.Api.Features.DeleteBasket;

public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool Success);

public class DeleteBasketCommandHandler(
    ApplicationDbContext context) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await context.ShoppingCarts.Where(b => b.Username == request.Username).FirstOrDefaultAsync(cancellationToken);
        if (basket is not null)
        {
            context.ShoppingCarts.Remove(basket);
            await context.SaveChangesAsync(cancellationToken);
        }
        return new DeleteBasketResult(true);
    }
}
