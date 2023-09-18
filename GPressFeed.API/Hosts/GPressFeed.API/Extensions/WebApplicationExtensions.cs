using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GPressFeed.API.Extensions;

public static class WebApplicationExtensions
{
    public static void ApplyDbMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        scope.ServiceProvider.GetService<GPressFeedDbContext>().Database.Migrate();
    }
}
