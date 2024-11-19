//namespace Catalog.Api.Data.Extensions;

//public static class DatabaseExtensions
//{
//    public static async Task InitializeDatabaseAsync(this WebApplication app)
//    {
//        using var scope = app.Services.CreateScope();

//        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//        context.Database.MigrateAsync().GetAwaiter().GetResult();

//        await SeedDataAsync(context);

//    }

//    private static async Task SeedDataAsync(ApplicationDbContext context)
//    {
//        await SeedGenresAsync(context);
//        await SeedAuthorsAsync(context);
//        await SeedBooksAsync(context);

//    }

//    private static async Task SeedGenresAsync(ApplicationDbContext context)
//    {
//        if (!await context.Genres.AnyAsync())
//        {
//            await context.AddRangeAsync(InitialData.Genres);
//            await context.SaveChangesAsync();
//        }
//    }

//    private static async Task SeedAuthorsAsync(ApplicationDbContext context)
//    {
//        if (!await context.Authors.AnyAsync())
//        {
//            await context.AddRangeAsync(InitialData.Authors);
//            await context.SaveChangesAsync();
//        }
//    }

//    private static async Task SeedBooksAsync(ApplicationDbContext context)
//    {
//        if (!await context.Books.AnyAsync())
//        {
//            await context.AddRangeAsync(InitialData.Books);
//            await context.SaveChangesAsync();
//        }
//    }
//}
