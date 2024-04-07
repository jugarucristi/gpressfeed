namespace Infrastructure.DTOs.OpenAi;

internal class OpenAiRequestDTO
{
    public string model { get; set; }

    public string prompt { get; set; }

    public int max_tokens { get; set; }
}
