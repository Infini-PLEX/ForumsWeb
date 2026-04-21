using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ForumsWeb.Data;
using ForumsWeb.Models;
using Microsoft.AspNetCore.Identity;

namespace ForumsWeb.Pages.Admin;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public int UserCount { get; set; }
    public int ForumCount { get; set; }
    public int PostCount { get; set; }
    public int ReplyCount { get; set; }

    public async Task OnGetAsync()
    {
        UserCount = await _userManager.Users.CountAsync();
        ForumCount = await _context.Forums.CountAsync();
        PostCount = await _context.Posts.CountAsync();
        ReplyCount = await _context.Replies.CountAsync();
    }
}
