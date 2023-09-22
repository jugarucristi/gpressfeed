using Application.Interfaces;
using Application.Models;

namespace Application.Services;

public class PressFeedService : IPressFeedService
{
    private readonly IGoogleTrendsRetriever _googleTrendsRetriever;
    private readonly IPressFeedRepository _repository;

    public PressFeedService(IGoogleTrendsRetriever googleTrendsRetriever, IPressFeedRepository repository)
    {
        _googleTrendsRetriever = googleTrendsRetriever;
        _repository = repository;
    }

    public async Task<Feed> UpsertAndReturnTodaysNewsAsync()
    {
        var currentFeed = await _repository.GetCurrentNewsFeedAsync();

        if (currentFeed == null)
        {
            var articleList = await _googleTrendsRetriever.GetNews();

            Feed newsFeed = null;
            if (articleList != null)
            {
                newsFeed = new Feed() { Articles = articleList };
                await _repository.InsertNewsFeedAsync(newsFeed);
            }

            currentFeed = newsFeed;
        }

        return currentFeed;
    }

    public async Task<List<Feed>> GetFeedHistoryAsync(int numberOfFeeds)
    {
        if (numberOfFeeds < 1)
        {
            throw new ArgumentOutOfRangeException("Number of articles must be greater or equal to 1");
        }

        var feedHistory = await _repository.GetFeedHistoryAsync(numberOfFeeds);

        return feedHistory;
    }

    public async Task<Feed> GetFeedByIdAsync(string feedId)
    {
        ArgumentNullException.ThrowIfNull(feedId);

        var feed = await _repository.GetFeedByIdAsync(feedId);

        return feed;
    }
}
