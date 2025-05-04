using System.ComponentModel.DataAnnotations;

namespace _02;

public class Note
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Title { get; set; }

    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}