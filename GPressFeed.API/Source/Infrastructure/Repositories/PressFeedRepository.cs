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

    public async Task<List<Feed>> GetFeedHistoryAsync(int numberOfFeeds)
    {
        if (numberOfFeeds < 1)
        {
            throw new ArgumentOutOfRangeException("Number of articles must be greater or equal to 1");
        }

        var result = await _context.Feeds
            .OrderByDescending(x => x.PublishDate)
            .GroupBy(x => x.PublishDate.Day)
            .Select(x => x.First())
            .ToListAsync();

        if (result.Count < numberOfFeeds)
        {
            return result;
        }
        else
        {
            return result.GetRange(0, numberOfFeeds);
        }
    }

    public async Task<Feed> GetFeedByIdAsync(string feedId)
    {
        ArgumentNullException.ThrowIfNull(feedId);

        var result = await _context.Feeds
            .Where(x => x.Id.ToString() == feedId)
            .Include(x => x.Articles)
            .FirstAsync();

        return result;
    }
}
