using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ForumsWeb.Data;
using ForumsWeb.Models;
using System.Security.Claims;

namespace ForumsWeb.Pages.Posts;

[Authorize]
public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(ApplicationDbContext context, ILogger<CreateModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    [BindProperty(SupportsGet = true)]
    public int ForumId { get; set; }

    [BindProperty]
    public Post Post { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Post.CreatedAt = DateTime.UtcNow;
        Post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        _context.Posts.Add(Post);
        await _context.SaveChangesAsync();

        _logger.LogInformation("User created a new post with ID: {Id} in Forum: {ForumId}", Post.Id, Post.ForumId);

        return RedirectToPage("./Index", new { forumId = Post.ForumId });
    }
}
