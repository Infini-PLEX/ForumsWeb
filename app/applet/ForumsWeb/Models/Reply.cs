using System.ComponentModel.DataAnnotations;

namespace ForumsWeb.Models;

public class Reply
{
    public int Id { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int PostId { get; set; }
    public Post? Post { get; set; }

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
}
