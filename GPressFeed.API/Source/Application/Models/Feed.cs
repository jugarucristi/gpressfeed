using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class Feed
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public List<Article> Articles { get; set; }

    public DateTime PublishDate { get; set; } = DateTime.Now;
}
