using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Infrastructure.DTOs;
using System.Text.Json;

namespace Infrastructure.Retrievers;

public class GoogleTrendsRetriever : IGoogleTrendsRetriever
{
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public GoogleTrendsRetriever(IMapper mapper, HttpClient httpClient)
    {
        _mapper = mapper;
        _httpClient = httpClient;
    }

	public async Task<List<Article>> GetNews()
	{
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, "/trends/api/dailytrends?geo=RO");

        var response = await _httpClient.SendAsync(httpRequest);
        var body = await response.Content.ReadAsStringAsync();

        var firstOcurrence = body.IndexOf("[");
        var lastOcurrence = body.LastIndexOf("]");

        body = body.Substring(firstOcurrence, lastOcurrence - firstOcurrence + 1);
        var googleNews = JsonSerializer.Deserialize<List<TrendingSearches>>(body);
        
        var refinedList = new List<Article>();
        googleNews.LastOrDefault().trendingSearches.ForEach(
                x => refinedList.Add(
                    _mapper.Map<Article>(x.articles[0])
                    )
                );

        return refinedList;
    }
}
