﻿using Application.Interfaces;
using Application.Models;

namespace Application.Services;

public class PressFeedService : IPressFeedService
{
    private readonly IGoogleTrendsRetriever _googleTrendsRetriever;
    private readonly IPressFeedRepository _repository;
    private readonly ICategoryRetriever _paLMRetriever;

    public PressFeedService(
        IGoogleTrendsRetriever googleTrendsRetriever, 
        IPressFeedRepository repository, 
        ICategoryRetriever paLMRetriever
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

    public async Task UpsertTodaysNewsAsync()
    {
        var currentFeed = await _repository.GetCurrentNewsFeedAsync();

        if (currentFeed == null)
        {
            var articleList = await _googleTrendsRetriever.GetNews();

            var newsFeed = await _paLMRetriever.GetFeedWithArticleCategories(articleList);
            await _repository.InsertNewsFeedAsync(newsFeed);
        }
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
