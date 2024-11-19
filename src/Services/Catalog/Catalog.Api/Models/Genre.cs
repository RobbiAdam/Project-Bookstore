namespace Catalog.Api.Models;

public class Genre
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
}