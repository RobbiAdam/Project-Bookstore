namespace Catalog.Api.Features.Books.DeleteBook;

public record DeleteBookCommand(
    Guid Id) : ICommand<DeleteBookResult>;

public record DeleteBookResult(
    bool Success);

public class DeleteBookCommandHandler(
    ApplicationDbContext context) : ICommandHandler<DeleteBookCommand, DeleteBookResult>
{
    public async Task<DeleteBookResult> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
    {
        var bookId = command.Id;
        var book = await context.Books.FindAsync([bookId], cancellationToken : cancellationToken);

        if (book is null)
        {
            return new DeleteBookResult(false);
        }

        context.Books.Remove(book);
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteBookResult(true);
    }
}
