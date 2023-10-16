namespace Infrastructure.DTOs;

internal class PaLMRequestDTO
{
    public Prompt prompt { get; set; } = new Prompt();

    public List<SafetySettings> safetySettings { get; } 
        = new List<SafetySettings>()
        {
            new SafetySettings()
            {
                category = "HARM_CATEGORY_DEROGATORY"
            },
            new SafetySettings()
            {
                category = "HARM_CATEGORY_TOXICITY"
            },
            new SafetySettings()
            {
                category = "HARM_CATEGORY_VIOLENCE"
            },
            new SafetySettings()
            {
                category = "HARM_CATEGORY_SEXUAL"
            },
            new SafetySettings()
            {
                category = "HARM_CATEGORY_MEDICAL"
            },
            new SafetySettings()
            {
                category = "HARM_CATEGORY_DANGEROUS"
            }
        };
}

internal class Prompt
{
    public string text { get; set; }
}

internal class SafetySettings
{
    public string category { get; set; }

    public string threshold { get; } = "BLOCK_NONE";
}