using Application.Models;

namespace Application.Interfaces;

public interface IPressFeedRepository
{
    Task<Feed> GetCurrentNewsFeedAsync();

    Task InsertNewsFeedAsync(Feed feed);

    Task<Feed> GetLatestAvailableFeed();

    Task<List<Feed>> GetFeedHistoryAsync(int numberOfFeeds);

    Task<Feed> GetFeedByIdAsync(string id);
}
