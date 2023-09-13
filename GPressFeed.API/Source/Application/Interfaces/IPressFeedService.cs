using Application.Models;

namespace Application.Interfaces;

public interface IPressFeedService
{
    Task<Feed> UpsertAndReturnTodaysNewsAsync();
}
