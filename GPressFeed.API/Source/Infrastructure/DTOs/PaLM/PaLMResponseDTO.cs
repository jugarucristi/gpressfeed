﻿namespace Infrastructure.DTOs.PaLM;

internal class PaLMResponseDTO
{
    public List<Candidate> candidates { get; set; }
}

internal class Candidate
{
    public string output { get; set; }
}