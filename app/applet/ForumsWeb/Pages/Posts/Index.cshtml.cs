using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ForumsWeb.Data;
using ForumsWeb.Models;

namespace ForumsWeb.Pages.Posts;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Post> Posts { get; set; } = default!;
    public Forum? Forum { get; set; }

    [BindProperty(SupportsGet = true)]
    public int ForumId { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Forum = await _context.Forums.FirstOrDefaultAsync(m => m.Id == ForumId);

        if (Forum == null)
        {
            return NotFound();
        }

        Posts = await _context.Posts
            .Where(p => p.ForumId == ForumId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return Page();
    }
}
