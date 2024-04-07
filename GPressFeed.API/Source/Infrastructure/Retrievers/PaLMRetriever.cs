using Application.Interfaces;
using Application.Models;
using Infrastructure.Configuration;
using Infrastructure.DTOs.PaLM;
using System.Text.Json;

namespace Infrastructure.Retrievers;

public class PaLMRetriever : ICategoryRetriever
{
    private readonly PaLMRetrieverConfiguration _configuration;
    private readonly HttpClient _httpClient;

	public PaLMRetriever(PaLMRetrieverConfiguration configuration, HttpClient httpClient)
	{
        _configuration = configuration;
        _httpClient = httpClient;
    }

    public async Task<Feed> GetFeedWithArticleCategories(List<Article> articleList)
    {
        var feed = new Feed() { Articles = articleList };

        foreach (var article in articleList)
        {
            try
            {
                article.Category = await GetNewsCategoryAsync(article.Title);
            }
            catch
            {
                article.Category = "Unknown";
            }
        }

        return feed;
    }

    private async Task<string> GetNewsCategoryAsync(string title)
    {
        var requestContent = new PaLMRequestDTO();
        requestContent.prompt.text = _configuration.Prompt + " " + title;

        var request = new HttpRequestMessage(HttpMethod.Post,
            "/v1beta2/models/" + _configuration.Model + ":generateText?key=" + _configuration.ApiKey);
        request.Content = new StringContent(JsonSerializer.Serialize(requestContent));

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();

        var mappedResponse = JsonSerializer.Deserialize<PaLMResponseDTO>(responseString);

        return mappedResponse.candidates[0].output;
    }
}
