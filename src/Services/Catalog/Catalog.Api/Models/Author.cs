namespace Catalog.Api.Models;

public class Author
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
}
