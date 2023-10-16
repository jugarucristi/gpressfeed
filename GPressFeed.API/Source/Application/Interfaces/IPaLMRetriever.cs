using Application.Models;

namespace Application.Interfaces;

public interface IPaLMRetriever
{
    public Task<Feed> GetFeedWithArticleCategories(List<Article> articleList);
}
