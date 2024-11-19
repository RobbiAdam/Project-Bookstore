namespace Catalog.Api.Features.Genres.DeleteGenre;

public record DeleteGenreCommand(Guid Id) : ICommand<DeleteGenreResult>;

public record DeleteGenreResult(bool Success);
public class DeleteGenreCommandHandler(
    ApplicationDbContext context) : ICommandHandler<DeleteGenreCommand, DeleteGenreResult>
{
    public async Task<DeleteGenreResult> Handle(DeleteGenreCommand command, CancellationToken cancellationToken)
    {
        var genreId = command.Id;
        var genre = await context.Genres.FindAsync([genreId], cancellationToken : cancellationToken);

        if (genre is null)
        {
            return new DeleteGenreResult(false);
        }

        context.Remove(genre);
        await context.SaveChangesAsync(cancellationToken);
        return new DeleteGenreResult(true);

    }
}
