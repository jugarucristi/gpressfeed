using Application.Models;

namespace Application.Interfaces;

public interface IPressFeedService
{
    Task<Feed> GetLatestNewsAsync();

    Task UpsertTodaysNewsAsync();

    Task<List<Feed>> GetFeedHistoryAsync(int numberOfFeeds);

    Task<Feed> GetFeedByIdAsync(string id);
}
