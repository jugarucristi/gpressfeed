using Application.Interfaces;
using Application.Models;

namespace Application.Services;

public class PressFeedService : IPressFeedService
{
    private readonly IGoogleTrendsRetriever _googleTrendsRetriever;
    private readonly IPressFeedRepository _repository;
    private readonly IPaLMRetriever _paLMRetriever;

    public PressFeedService(
        IGoogleTrendsRetriever googleTrendsRetriever, 
        IPressFeedRepository repository, 
        IPaLMRetriever paLMRetriever
        )
    {
        _googleTrendsRetriever = googleTrendsRetriever;
        _repository = repository;
        _paLMRetriever = paLMRetriever;
    }

    public async Task<Feed> GetLatestNewsAsync()
    {
        var currentFeed = await _repository.GetLatestAvailableFeed();

        return currentFeed;
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
                newsFeed = await _paLMRetriever.GetFeedWithArticleCategories(articleList);
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
