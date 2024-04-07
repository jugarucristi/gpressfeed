using Application.Interfaces;
using Application.Models;
using Infrastructure.Configuration;
using Infrastructure.DTOs.OpenAi;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Infrastructure.Retrievers;

public class OpenAIRetriever : ICategoryRetriever
{
    public readonly OpenAiRetrieverConfiguration _configuration;
    public readonly HttpClient _httpClient;

    public OpenAIRetriever(OpenAiRetrieverConfiguration configuration, HttpClient httpClient)
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
        var requestContent = new OpenAiRequestDTO();
        requestContent.prompt = _configuration.Prompt + " " + title;
        requestContent.model = _configuration.Model;
        requestContent.max_tokens = _configuration.MaxTokens;

        var request = new HttpRequestMessage(HttpMethod.Post, "v1/completions");

        var jsonSerializerOptions = new JsonSerializerOptions() {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        var content = JsonSerializer.Serialize(requestContent, jsonSerializerOptions);
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");
        request.Headers.Add("Authorization", "Bearer " + _configuration.ApiKey);

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();

        var mappedResponse = JsonSerializer.Deserialize<OpenAiResponseDTO>(responseString);

        var result = mappedResponse.choices[0].text.Replace("\n", String.Empty);

        return result;
    }
}
