namespace Catalog.Api.Features.Books.GetBooks;

public record GetBooksQuery() : IQuery<GetBooksResult>;

public record GetBooksResult(
    IEnumerable<BookDto> Books);

public class GetBooksQueryHandler(
    ApplicationDbContext context) : IQueryHandler<GetBooksQuery, GetBooksResult>
{
    public async Task<GetBooksResult> Handle(GetBooksQuery query, CancellationToken cancellationToken)
    {
        var books = await context.Books
            .AsNoTracking()
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);

        var bookDtos = books.Select(b => b.Adapt<BookDto>());

        return new GetBooksResult(bookDtos);
    }
}
