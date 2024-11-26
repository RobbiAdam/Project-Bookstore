namespace Catalog.Api.Features.Books.UpdateBook;
public record UpdateBookCommand(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    IEnumerable<Guid> AuthorsIds,
    IEnumerable<Guid> GenresIds) : ICommand<UpdateBookResult>;

public record UpdateBookResult(bool Success);

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");

        RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
        RuleFor(x => x.Price).Must(x => x > 0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.AuthorsIds).NotEmpty().WithMessage("Authors is required");
        RuleFor(x => x.GenresIds).NotEmpty().WithMessage("Genres is required");
    }
}

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