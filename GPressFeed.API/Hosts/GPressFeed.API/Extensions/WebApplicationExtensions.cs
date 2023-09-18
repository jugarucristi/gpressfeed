using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GPressFeed.API.Extensions;

public static class WebApplicationExtensions
{
    public static void ApplyDbMigrations(this WebApplication app)
    {
        app.Services.GetService<GPressFeedDbContext>().Database.Migrate();
    }
}
