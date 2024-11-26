namespace Catalog.Api.Features.Products.CreateProduct;

public record CreateBookCommand(
    string Title,
    string Description,
    decimal Price,
    IEnumerable<Guid> AuthorsIds,
    IEnumerable<Guid> GenresIds)
    : ICommand<CreateBookResult>;

public record CreateBookResult(
    Guid Id);

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");

        RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
        RuleFor(x => x.Price).Must(x => x > 0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.AuthorsIds).NotEmpty().WithMessage("Authors is required");
        RuleFor(x => x.GenresIds).NotEmpty().WithMessage("Genres is required");
    }
}

internal class CreateBookCommandHandler(
    ApplicationDbContext context) : ICommandHandler<CreateBookCommand, CreateBookResult>
{
    public async Task<CreateBookResult> Handle(CreateBookCommand command, CancellationToken cancellationToken)
    {
        var authorIds = command.AuthorsIds ?? Enumerable.Empty<Guid>();
        var genreIds = command.GenresIds ?? Enumerable.Empty<Guid>();

        var authors = !authorIds.Any()
            ? new List<Author>()
            : await context.Authors
                .Where(x => authorIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

        var genres = !genreIds.Any()
            ? new List<Genre>()
            : await context.Genres
                .Where(x => genreIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

        if (authorIds.Any() && authors.Count != authorIds.Count())
        {
            throw new Exception("Authors not found");
        }

        if (genreIds.Any() && genres.Count != genreIds.Count())
        {
            throw new Exception("Genres not found");
        }

        var book = new Book
        {
            Title = command.Title,
            Description = command.Description,
            Price = command.Price,
            Genres = genres,
            Authors = authors
        };     
  
        context.Books.Add(book);
        await context.SaveChangesAsync(cancellationToken);
        return new CreateBookResult(book.Id);
    }
}
