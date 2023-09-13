using Application.Interfaces;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PressFeedRepository : IPressFeedRepository
{
    private readonly GPressFeedDbContext _context;

    public PressFeedRepository(GPressFeedDbContext context)
    {
        _context = context;
    }

    public async Task InsertNewsFeedAsync(Feed feed)
    {
        _context.Feeds.Add(feed);
        await _context.SaveChangesAsync();
    }

    public async Task<Feed> GetCurrentNewsFeedAsync()
    {
        var currentDate = DateTime.Now.AddHours(-6);

        var result = await _context.Feeds
            .Where(x =>
        x.PublishDate.Day == currentDate.Day &&
        x.PublishDate.Month == currentDate.Month &&
        x.PublishDate.Year == currentDate.Year)
            .Include(x => x.Articles)
            .FirstOrDefaultAsync();

        return result;
    }
}
