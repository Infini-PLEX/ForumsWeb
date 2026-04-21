using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ForumsWeb.Models;

namespace ForumsWeb.Pages.Admin.Users;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IndexModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public IList<ApplicationUser> UsersList { get; set; } = default!;

    public async Task OnGetAsync()
    {
        UsersList = await _userManager.Users.ToListAsync();
    }

    public async Task<IActionResult> OnPostBanAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null && user.Email != "admin@example.com")
        {
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUnbanAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            await _userManager.SetLockoutEndDateAsync(user, null);
        }
        return RedirectToPage();
    }
}
