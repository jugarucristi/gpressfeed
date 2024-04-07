namespace Infrastructure.Configuration;

public class OpenAiRetrieverConfiguration
{
    public string BaseAddress { get; set; }

    public string ApiKey { get; set; }

    public string Model { get; set; }

    public int MaxTokens { get; set; }

    public string Prompt { get; } =
        "Given 7 categories: World News, Politics, " +
        "Finance, Science, Entertainment, Sports, " +
        "in what category would you fit this headline:";
}
