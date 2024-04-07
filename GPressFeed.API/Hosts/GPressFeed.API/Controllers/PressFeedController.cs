using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPressFeed.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PressFeedController : ControllerBase
{
    private readonly IPressFeedService _pressFeedService;

    public PressFeedController(IPressFeedService pressFeedService)
    {
        _pressFeedService = pressFeedService;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetNewsByIdAsync([FromQuery] string feedId)
    {
        if (feedId == null)
        {
            return BadRequest();
        }

        var result = await _pressFeedService.GetFeedByIdAsync(feedId);

        return Ok(result);
    }

    [HttpGet("today")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetTodaysNewsAsync()
    {
        var result = await _pressFeedService.GetLatestNewsAsync();

        return Ok(result);
    }

    [HttpGet("history")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetFeedHistoryAsync([FromQuery] int numberOfFeeds)
    {
        if(numberOfFeeds < 1)
        {
            return BadRequest();
        }

        var result = await _pressFeedService.GetFeedHistoryAsync(numberOfFeeds);

        return Ok(result);
    }
}