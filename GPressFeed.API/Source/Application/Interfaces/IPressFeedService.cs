using Application.Models;

namespace Application.Interfaces;

public interface IPressFeedService
{
    Task<Feed> GetLatestNewsAsync();

    Task<Feed> UpsertAndReturnTodaysNewsAsync();

    Task<List<Feed>> GetFeedHistoryAsync(int numberOfFeeds);

    Task<Feed> GetFeedByIdAsync(string id);
}
