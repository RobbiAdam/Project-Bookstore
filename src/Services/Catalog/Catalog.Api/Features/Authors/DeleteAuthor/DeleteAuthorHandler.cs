namespace Catalog.Api.Features.Authors.DeleteAuthor;

public record DeleteAuthorCommand(Guid Id) : ICommand<DeleteAuthorResult>;

public record DeleteAuthorResult(bool Success);


public class DeleteAuthorCommandHandler(
    ApplicationDbContext context) : ICommandHandler<DeleteAuthorCommand, DeleteAuthorResult>
{
    public async Task<DeleteAuthorResult> Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
    {
        var authorId = command.Id;
        var author = await context.Authors.FindAsync([authorId], cancellationToken: cancellationToken);
        if (author is null)
        {
            return new DeleteAuthorResult(false);
        }

        context.Remove(author);
        await context.SaveChangesAsync(cancellationToken);
        return new DeleteAuthorResult(true);
    }
}
