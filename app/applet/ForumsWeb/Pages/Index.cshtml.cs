using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ForumsWeb.Data;
using ForumsWeb.Models;

namespace ForumsWeb.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IList<Forum> Forums { get; set; } = default!;

    public async Task OnGetAsync()
    {
        _logger.LogInformation("Loading forums for the homepage.");
        Forums = await _context.Forums.ToListAsync();
    }
}
