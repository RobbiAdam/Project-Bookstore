namespace Catalog.Api.Dtos;

public record BookDto(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    IEnumerable<AuthorDto> Authors,
    IEnumerable<GenreDto> Genres);

public record BookUpdateDto(
    string Title,
    string Description,
    decimal Price,
    IEnumerable<AuthorDto> Authors,
    IEnumerable<GenreDto> Genres);