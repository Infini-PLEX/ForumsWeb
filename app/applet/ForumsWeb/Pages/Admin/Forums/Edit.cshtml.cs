using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ForumsWeb.Data;
using ForumsWeb.Models;

namespace ForumsWeb.Pages.Admin.Forums;

[Authorize(Roles = "Admin")]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Forum Forum { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        var forum =  await _context.Forums.FirstOrDefaultAsync(m => m.Id == id);
        if (forum == null) return NotFound();

        Forum = forum;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        _context.Attach(Forum).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ForumExists(Forum.Id)) return NotFound();
            else throw;
        }

        return RedirectToPage("./Index");
    }

    private bool ForumExists(int id)
    {
        return _context.Forums.Any(e => e.Id == id);
    }
}
