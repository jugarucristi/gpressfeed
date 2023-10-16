using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class Article
{
    [Key]
    public Guid Id { get; set;  } = Guid.NewGuid();

    public string Title { get; set; }

    public string Link { get; set; }

    public string Category { get; set; }
}
