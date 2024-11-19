namespace Catalog.Api.Features.Authors.GetAuthors;

public record GetAuthorsQuery() : IQuery<GetAuthorsResult>;

public record GetAuthorsResult(IEnumerable<Author> Authors);
public class GetAuthorsQueryHandler(
    ApplicationDbContext context) : IQueryHandler<GetAuthorsQuery, GetAuthorsResult>
{
    public async Task<GetAuthorsResult> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await context.Authors
            .AsNoTracking()
            .ToListAsync(cancellationToken);


        return new GetAuthorsResult(authors);
    }
}
