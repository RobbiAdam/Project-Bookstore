namespace Catalog.Api.Features.Genres.UpdateGenre;

public record class UpdateGenreCommand(Guid Id, string Name) : ICommand<UpdateGenreResult>;
public record class UpdateGenreResult(bool Success);

public class UpdateGenreCommandHandler(
    ApplicationDbContext context) : ICommandHandler<UpdateGenreCommand, UpdateGenreResult>
{
    public async Task<UpdateGenreResult> Handle(UpdateGenreCommand command, CancellationToken cancellationToken)
    {
        var genreId = command.Id;

        var genre = await context.Genres.FindAsync([genreId], cancellationToken : cancellationToken);

        if (genre is null)
        {
            return new UpdateGenreResult(false);
        }

        genre.Name = command.Name;

        context.Genres.Update(genre);
        await context.SaveChangesAsync(cancellationToken);
        return new UpdateGenreResult(true);


    }
}
