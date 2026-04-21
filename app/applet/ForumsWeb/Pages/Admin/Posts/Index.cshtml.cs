using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ForumsWeb.Data;
using ForumsWeb.Models;

namespace ForumsWeb.Pages.Admin.Posts;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Post> Posts { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Posts = await _context.Posts
            .Include(p => p.Forum)
            .Include(p => p.User)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int? id)
    {
        if (id == null) return NotFound();

        var post = await _context.Posts.FindAsync(id);

        if (post != null)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
