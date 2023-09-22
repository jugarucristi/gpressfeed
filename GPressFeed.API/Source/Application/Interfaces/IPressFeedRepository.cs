﻿using Application.Models;

namespace Application.Interfaces;

public interface IPressFeedRepository
{
    Task<Feed> GetCurrentNewsFeedAsync();

    Task InsertNewsFeedAsync(Feed feed);

    Task<List<Feed>> GetFeedHistoryAsync(int numberOfFeeds);

    Task<Feed> GetFeedByIdAsync(string id);
}
