namespace Infrastructure.DTOs;

public class TrendingSearches
{
    public List<TrendingSearch> trendingSearches { get; set; }

    public string date { get; set;}
}

public class TrendingSearch
{
    public List<TrendingArticle> articles { get; set; }
}

public class TrendingArticle
{
    public string title { get; set; }

    public string url { get; set; }
}
