namespace Catalog.Api.Features.Books.GetBook;

public record GetBookByIdQuery(
    Guid Id) : IQuery<GetBookByIdResult>;

public record GetBookByIdResult(
    BookDto Book);

public class GetBookByIdQueryHandler(
    ApplicationDbContext context) : IQueryHandler<GetBookByIdQuery, GetBookByIdResult>
{
    public async Task<GetBookByIdResult> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
    {
        var book = await context.Books
            .Where(x => x.Id == query.Id)
            .AsNoTracking()
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken);


        if (book is null)
        {
            throw new NullReferenceException("Book not found");
        }

        var bookDto = book.Adapt<BookDto>();

        return new GetBookByIdResult(bookDto);
    }
}
