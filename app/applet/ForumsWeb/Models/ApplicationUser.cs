using Microsoft.AspNetCore.Identity;

namespace ForumsWeb.Models;

public class ApplicationUser : IdentityUser
{
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
}
