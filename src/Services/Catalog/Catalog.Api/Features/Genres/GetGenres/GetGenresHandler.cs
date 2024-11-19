namespace Catalog.Api.Features.Genres.GetGenres;

public record GetGenresQuery() : IQuery<GetGenresResult>;
public record GetGenresResult(IEnumerable<Genre> Genres);
public class GetGenresQueryHandler(
    ApplicationDbContext context) : IQueryHandler<GetGenresQuery, GetGenresResult>
{
    public async Task<GetGenresResult> Handle(GetGenresQuery query, CancellationToken cancellationToken)
    {
        var genres = await context.Genres
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new GetGenresResult(genres);
    }
}
