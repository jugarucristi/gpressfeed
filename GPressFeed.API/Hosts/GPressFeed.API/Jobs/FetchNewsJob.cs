using Application.Interfaces;
using Quartz;

namespace GPressFeed.API.Jobs;

[DisallowConcurrentExecution]
public class FetchNewsJob : IJob
{
    private readonly IPressFeedService _pressFeedService;

    public FetchNewsJob(IPressFeedService pressFeedService)
    {
        _pressFeedService = pressFeedService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _pressFeedService.UpsertTodaysNewsAsync();
    }
}
