using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class GPressFeedDbContext : DbContext
{
    public GPressFeedDbContext(DbContextOptions<GPressFeedDbContext> options) : base(options)
    {
    }

    public DbSet<Feed> Feeds { get; set; }

    public DbSet<Article> Articles { get; set; }
}
