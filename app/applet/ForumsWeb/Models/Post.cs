using System.ComponentModel.DataAnnotations;

namespace ForumsWeb.Models;

public class Post
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int ForumId { get; set; }
    public Forum? Forum { get; set; }

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public List<Reply> Replies { get; set; } = new();
}
