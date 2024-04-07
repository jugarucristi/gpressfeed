namespace Infrastructure.DTOs.OpenAi;

internal class OpenAiResponseDTO
{
    public List<Choices> choices { get; set; }
}

internal class Choices
{
    public string text { get; set; }
}
