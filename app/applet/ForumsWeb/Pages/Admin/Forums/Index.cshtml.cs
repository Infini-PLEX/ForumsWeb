using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ForumsWeb.Data;
using ForumsWeb.Models;

namespace ForumsWeb.Pages.Admin.Forums;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Forum> Forum { get;set; } = default!;

    public async Task OnGetAsync()
    {
        Forum = await _context.Forums.ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int? id)
    {
        if (id == null) return NotFound();

        var forum = await _context.Forums.FindAsync(id);

        if (forum != null)
        {
            _context.Forums.Remove(forum);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
