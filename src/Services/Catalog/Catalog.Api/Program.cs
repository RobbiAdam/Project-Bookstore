using Catalog.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services    
    .AddCatalogApi(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    await app.InitializeDatabaseAsync();
//}
app.UseCatalogApi();

app.Run();

