using Basket.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddBasketApi(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseBasketApi();

app.Run();

