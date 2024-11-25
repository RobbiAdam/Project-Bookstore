using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Catalog.Api.Features.EventsHandler;

public class GetBookInfoConsumer(
    ApplicationDbContext dbContext) : IConsumer<GetBookInfoEventRequest>
{
    public async Task Consume(ConsumeContext<GetBookInfoEventRequest> context)
    {
        var book = await dbContext.Books
            .AsNoTracking()
            .Where(x => x.Id == context.Message.BookId)
            .Select(x => new {x.Title, x.Price})
            .FirstOrDefaultAsync(context.CancellationToken);

        if (book == null)
        {
            throw new InvalidOperationException($"Book with Id {context.Message.BookId} not found");
        }

        await context.RespondAsync(new GetBookInfoEventResponse(
            Title: book.Title,
            Price: book.Price));
    }
}
