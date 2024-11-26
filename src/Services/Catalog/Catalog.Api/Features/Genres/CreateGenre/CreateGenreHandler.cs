namespace Catalog.Api.Features.Genres.CreateGenre;

public record CreateGenreCommand(string Name) : ICommand<CreateGenreResult>;

public record CreateGenreResult(Guid Id);

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Genre name is required.");
    }
}

public class CreateGenreCommandHandler(
    ApplicationDbContext context) : ICommandHandler<CreateGenreCommand, CreateGenreResult>
{
    public async Task<CreateGenreResult> Handle(CreateGenreCommand command, CancellationToken cancellationToken)
    {
        var genre = new Genre
        {
            Name = command.Name
        };

        context.Genres.Add(genre);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateGenreResult(genre.Id);
    }
}
