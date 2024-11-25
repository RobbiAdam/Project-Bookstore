using BuildingBlocks.Messaging.MassTransit;
using System.Reflection;

namespace Basket.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddBasketApi(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddCarter()
            .AddMediator()
            .AddDatabase(configuration)
            .AddMessageBroker(configuration, Assembly.GetExecutingAssembly())
            .AddServices();
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Database")));
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IBasketService, BasketService>();
    }

    public static WebApplication UseBasketApi(this WebApplication app)
    {
        app.MapCarter();
        return app;
    }
}
