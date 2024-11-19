namespace Catalog.Api.Models;

public class Book
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = null!; 
    public string Description { get; set; } = null!; 
    public decimal Price { get; set; }

    public List<Author> Authors { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();

}
