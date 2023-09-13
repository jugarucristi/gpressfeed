using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Infrastructure.DTOs;
using System.Text.Json;

namespace Infrastructure.Retrievers;

public class GoogleTrendsRetriever : IGoogleTrendsRetriever
{
    private readonly IMapper _mapper;

    public GoogleTrendsRetriever(IMapper mapper)
    {
        _mapper = mapper;
    }

	public async Task<List<Article>> GetNews()
	{
        var httpClient = new HttpClient();

        httpClient.BaseAddress = new Uri("https://trends.google.com");
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, "/trends/api/dailytrends?geo=RO");

        var response = await httpClient.SendAsync(httpRequest);
        var body = await response.Content.ReadAsStringAsync();

        var firstOcurrence = body.IndexOf("[");
        var lastOcurrence = body.LastIndexOf("]");

        body = body.Substring(firstOcurrence, lastOcurrence - firstOcurrence + 1);
        var googleNews = JsonSerializer.Deserialize<List<TrendingSearches>>(body);
        
        var refinedList = new List<Article>();
        googleNews.ElementAt(1).trendingSearches.ForEach(
                x => refinedList.Add(
                    _mapper.Map<Article>(x.articles[0])
                    )
                );

        return refinedList;
    }
}
