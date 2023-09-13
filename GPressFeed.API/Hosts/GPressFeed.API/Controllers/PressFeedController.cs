using Application.Interfaces;
using Application.Models;
using Application.Services;
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
    public async Task<IActionResult> GetTodaysNews()
    {
        var result = await _pressFeedService.UpsertAndReturnTodaysNewsAsync();

        return Ok(result);
    }
}