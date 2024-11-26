using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using System.Reflection;

namespace Catalog.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogApi(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddCarter()
            .AddMediator()
            .AddDatabase(configuration)
            .AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

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
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            })
        .AddValidatorsFromAssembly(typeof(Program).Assembly);
    }


    public static WebApplication UseCatalogApi(this WebApplication app)
    {
        app.MapCarter();
        return app;
    }



}
