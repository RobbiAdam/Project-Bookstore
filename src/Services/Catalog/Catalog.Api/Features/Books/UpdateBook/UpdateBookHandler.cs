namespace Catalog.Api.Features.Books.UpdateBook;
public record UpdateBookCommand(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    IEnumerable<Guid> AuthorsIds,
    IEnumerable<Guid> GenresIds) : ICommand<UpdateBookResult>;

public record UpdateBookResult(bool Success);

public class UpdateBookCommandHandler(
    ApplicationDbContext context) : ICommandHandler<UpdateBookCommand, UpdateBookResult>
{
    public async Task<UpdateBookResult> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var bookToUpdate = await context.Books
            .Include(x => x.Authors)
            .Include(x => x.Genres)
            .AsSplitQuery()            
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        var authors = command.AuthorsIds.Any() ?
            await context.Authors.Where(x => command.AuthorsIds.Contains(x.Id)).ToListAsync(cancellationToken) :
            new List<Author>();

        var genres = command.GenresIds.Any() ?
            await context.Genres.Where(x => command.GenresIds.Contains(x.Id)).ToListAsync(cancellationToken) :
            new List<Genre>();

        if (bookToUpdate is null)
        {
            return new UpdateBookResult(false);
        }

        bookToUpdate.Title = command.Title;
        bookToUpdate.Description = command.Description;
        bookToUpdate.Price = command.Price;
        bookToUpdate.Authors = authors;
        bookToUpdate.Genres = genres;

        context.Books.Update(bookToUpdate);

        await context.SaveChangesAsync(cancellationToken);
        return new UpdateBookResult(true);

    }


}