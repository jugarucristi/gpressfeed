using Application.Models;

namespace Application.Interfaces;

public interface IGoogleTrendsRetriever
{
    public Task<List<Article>> GetNews();
}
