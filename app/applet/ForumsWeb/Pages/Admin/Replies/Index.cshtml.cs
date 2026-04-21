using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ForumsWeb.Data;
using ForumsWeb.Models;

namespace ForumsWeb.Pages.Admin.Replies;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Reply> Replies { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Replies = await _context.Replies
            .Include(r => r.Post)
            .Include(r => r.User)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int? id)
    {
        if (id == null) return NotFound();

        var reply = await _context.Replies.FindAsync(id);

        if (reply != null)
        {
            _context.Replies.Remove(reply);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
