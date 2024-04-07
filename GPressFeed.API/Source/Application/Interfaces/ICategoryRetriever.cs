using Application.Models;

namespace Application.Interfaces;

public interface ICategoryRetriever
{
    public Task<Feed> GetFeedWithArticleCategories(List<Article> articleList);
}
