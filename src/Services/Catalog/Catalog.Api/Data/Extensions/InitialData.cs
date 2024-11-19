//namespace Catalog.Api.Data.Extensions;

//internal class InitialData
//{
//    public static IEnumerable<Genre> Genres => new List<Genre>()
//    {
//        new Genre() { Id = Guid.Parse("191b6466-c9d5-4672-b3df-7fcee3c7bbf3")},
//        new Genre() { Name = "Fantasy" },
//        new Genre() { Name = "Mystery" },
//        new Genre() { Name = "Romance" },
//        new Genre() { Name = "Horror" },
//        new Genre() { Name = "Historical Fiction" },
//        new Genre() { Name = "Biography" },
//        new Genre() { Name = "Self-Help" },
//        new Genre() { Name = "Technology" },
//        new Genre() { Name = "Business" }
//    };
//}

//    public static IEnumerable<Author> Authors => new List<Author>()
//    {
//        new Author() { Name = "Isaac Asimov" },
//        new Author() { Name = "J.R.R. Tolkien" },
//        new Author() { Name = "Agatha Christie" },
//        new Author() { Name = "Jane Austen" },
//        new Author() { Name = "Stephen King" },
//        new Author() { Name = "Ken Follett" },
//        new Author() { Name = "Michelle Obama" },
//        new Author() { Name = "Dale Carnegie" },
//        new Author() { Name = "Robert Martin" },
//        new Author() { Name = "Peter Thiel" },
//        new Author() { Name = "Neil Gaiman" },
//        new Author() { Name = "George R.R. Martin" }
//    };

//    public static IEnumerable<Book> Books => new List<Book>()
//    {
//        new Book() {
//            Title = "Foundation",
//            Description = "The story of the Foundation, an institute dedicated to preserving the best of human knowledge.",
//            Authors = new List<Author> { Authors.ElementAt(0) },
//            Genres = new List<Genre> { Genres.ElementAt(0) },
//            Price = 19.99m
//        },
//        new Book() {
//            Title = "The Lord of the Rings",
//            Description = "Epic high-fantasy novel that follows the quest to destroy the One Ring.",
//            Authors = new List<Author> { Authors.ElementAt(1) },
//            Genres = new List<Genre> { Genres.ElementAt(1) },
//            Price = 29.99m
//        },
//        new Book() {
//            Title = "Murder on the Orient Express",
//            Description = "Detective Hercule Poirot investigates a murder on a trapped train.",
//            Authors = new List<Author> { Authors.ElementAt(2) },
//            Genres = new List<Genre> { Genres.ElementAt(2) },
//            Price = 15.99m
//        },
//        new Book() {
//            Title = "Pride and Prejudice",
//            Description = "The story of Elizabeth Bennet and her prejudice against the proud Mr. Darcy.",
//            Authors = new List<Author> { Authors.ElementAt(3) },
//            Genres = new List<Genre> { Genres.ElementAt(3) },
//            Price = 12.99m
//        },
//        new Book() {
//            Title = "The Shining",
//            Description = "A family becomes caretakers of an isolated hotel for the winter.",
//            Authors = new List<Author> { Authors.ElementAt(4) },
//            Genres = new List<Genre> { Genres.ElementAt(4) },
//            Price = 18.99m
//        },
//        new Book() {
//            Title = "The Pillars of the Earth",
//            Description = "Epic historical novel about building a cathedral in the middle ages.",
//            Authors = new List<Author> { Authors.ElementAt(5) },
//            Genres = new List<Genre> { Genres.ElementAt(5) },
//            Price = 24.99m
//        },
//        new Book() {
//            Title = "Becoming",
//            Description = "Memoir of former First Lady Michelle Obama.",
//            Authors = new List<Author> { Authors.ElementAt(6) },
//            Genres = new List<Genre> { Genres.ElementAt(6) },
//            Price = 27.99m
//        },
//        new Book() {
//            Title = "How to Win Friends and Influence People",
//            Description = "Classic self-help book about interpersonal skills.",
//            Authors = new List<Author> { Authors.ElementAt(7) },
//            Genres = new List<Genre> { Genres.ElementAt(7) },
//            Price = 14.99m
//        },
//        new Book() {
//            Title = "Clean Code",
//            Description = "Guide to writing maintainable and scalable code.",
//            Authors = new List<Author> { Authors.ElementAt(8) },
//            Genres = new List<Genre> { Genres.ElementAt(8) },
//            Price = 39.99m
//        },
//        new Book() {
//            Title = "Zero to One",
//            Description = "Notes on startups and how to build the future.",
//            Authors = new List<Author> { Authors.ElementAt(9) },
//            Genres = new List<Genre> { Genres.ElementAt(9) },
//            Price = 22.99m
//        },
//        new Book() {
//            Title = "American Gods",
//            Description = "A blend of Americana, fantasy, and mythology.",
//            Authors = new List<Author> { Authors.ElementAt(10) },
//            Genres = new List<Genre> { Genres.ElementAt(0), Genres.ElementAt(1) },
//            Price = 17.99m
//        },
//        new Book() {
//            Title = "A Game of Thrones",
//            Description = "First book in the epic fantasy series A Song of Ice and Fire.",
//            Authors = new List<Author> { Authors.ElementAt(11) },
//            Genres = new List<Genre> { Genres.ElementAt(1), Genres.ElementAt(5) },
//            Price = 20.99m
//        }
//    };
//}