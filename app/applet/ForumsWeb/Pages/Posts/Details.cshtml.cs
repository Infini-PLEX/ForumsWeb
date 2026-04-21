using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ForumsWeb.Data;
using ForumsWeb.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ForumsWeb.Pages.Posts;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(ApplicationDbContext context, ILogger<DetailsModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Post? Post { get; set; }

    [BindProperty]
    public Reply Reply { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        Post = await _context.Posts
            .Include(p => p.Replies)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Post == null) return NotFound();
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null) return NotFound();

        if (!ModelState.IsValid)
        {
            Post = await _context.Posts
                .Include(p => p.Replies)
                .FirstOrDefaultAsync(m => m.Id == id);
            return Page();
        }

        Reply.CreatedAt = DateTime.UtcNow;
        Reply.PostId = id.Value;
        Reply.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        _context.Replies.Add(Reply);
        await _context.SaveChangesAsync();

        _logger.LogInformation("New reply added to Post {PostId}", Reply.PostId);

        return RedirectToPage("./Details", new { id = id });
    }
}
