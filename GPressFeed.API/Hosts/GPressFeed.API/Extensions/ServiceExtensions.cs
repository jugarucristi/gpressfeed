using Application.Interfaces;
using Application.Services;
using GPressFeed.API.Configurations;
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

    public static IServiceCollection AddGoogleTrendsRetriever(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IGoogleTrendsRetriever, GoogleTrendsRetriever>();

        var retrieverConfiguration = configuration
           .GetSection("GoogleTrendsRetrieverConfiguration")
           .Get<GoogleTrendsConfiguration>();
        services.AddHttpClient<IGoogleTrendsRetriever, GoogleTrendsRetriever>(client =>
        {
            client.BaseAddress = new Uri(retrieverConfiguration.BaseAddress);
        });

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

    public static IServiceCollection AddPressFeedCors(this IServiceCollection services, IConfiguration configuration)
    {
        var corsConfiguration = configuration.GetSection("CorsConfiguration").Get<CorsConfiguration>();

        services.AddCors(options =>
        {
            options.AddPolicy(name: corsConfiguration.Name,
                              policy =>
                              {
                                  policy.WithOrigins(corsConfiguration.OriginAddress);
                              });
        });

        return services;
    }
}
