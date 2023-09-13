using Application.Interfaces;
using Application.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Retrievers;
using Microsoft.EntityFrameworkCore;

namespace GPressFeed.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddTransientServices(this IServiceCollection services)
    {
        services.AddTransient<IPressFeedRepository, PressFeedRepository>();
        services.AddTransient<IPressFeedService, PressFeedService>();
        services.AddTransient<IGoogleTrendsRetriever, GoogleTrendsRetriever>();

        return services;
    }

    public static IServiceCollection AddPostgresDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresDb");

        services.AddDbContext<GPressFeedDbContext>(
        o => o.UseNpgsql(connectionString)
            );

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }
}
