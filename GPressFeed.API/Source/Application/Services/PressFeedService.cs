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
            var newsFeed = new Feed() { Articles = articleList };

            await _repository.InsertNewsFeedAsync(newsFeed);

            currentFeed = newsFeed;
        }

        return currentFeed;
    }
}
