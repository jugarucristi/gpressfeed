using Application.Interfaces;
using Application.Services;
using AutoMapper.Configuration.Annotations;
using GPressFeed.API.Configurations;
using GPressFeed.API.Jobs;
using Infrastructure;
using Infrastructure.Configuration;
using Infrastructure.Repositories;
using Infrastructure.Retrievers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

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

    public static IServiceCollection AddPaLMRetriever(this IServiceCollection services, IConfiguration configuration)
    {
        var retrieverConfiguration = configuration
           .GetSection("PaLMRetrieverConfiguraton")
           .Get<PaLMRetrieverConfiguration>();

        services.AddSingleton(retrieverConfiguration);
        services.AddTransient<ICategoryRetriever, PaLMRetriever>();
        services.AddHttpClient<ICategoryRetriever, PaLMRetriever>(client =>
        {
            client.BaseAddress = new Uri(retrieverConfiguration.BaseAddress);
        });

        return services;
    }

    public static IServiceCollection AddOpenAiRetriever(this IServiceCollection services, IConfiguration configuration)
    {
        var retrieverConfiguration = configuration
           .GetSection("OpenAiRetrieverConfiguration")
           .Get<OpenAiRetrieverConfiguration>();

        services.AddSingleton(retrieverConfiguration);
        services.AddTransient<ICategoryRetriever, OpenAIRetriever>();
        services.AddHttpClient<ICategoryRetriever, OpenAIRetriever>(client =>
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

    public static IServiceCollection AddFetchNewsJob(this IServiceCollection services, IConfiguration configuration)
    {
        var jobConfiguration = configuration.GetSection("FetchNewsJobConfiguration").Get<FetchNewsJobConfiguration>();

        services.AddQuartz(o =>
        {
            o.UseMicrosoftDependencyInjectionJobFactory();
            var jobKey = new JobKey(jobConfiguration.Name);
            o.AddJob<FetchNewsJob>(o => o.WithIdentity(jobKey));
            o.AddTrigger(o =>
                o.ForJob(jobKey)
                .WithIdentity(jobConfiguration.Name)
                .WithCronSchedule(jobConfiguration.CronSchedule)
            );
        });
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}
