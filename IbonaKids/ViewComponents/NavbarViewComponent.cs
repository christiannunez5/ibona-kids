using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IbonaKids.Data;
using IbonaKids.ViewModels;

namespace IbonaKids.ViewComponents;

public class NavbarViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public NavbarViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        double? balance = null;
        string? username = null;
        string? profileUrl = null;

        if (User.Identity?.IsAuthenticated == true)
        {
            var authenticatedUsername = User.Identity.Name;

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == authenticatedUsername);

            if (user != null)
            {
                balance = user.Balance;
                username = user.Username;
                profileUrl = user.ProfileUrl;
            }
        }

        var model = new NavbarViewModel
        {
            Balance = balance,
            Username = username,
            ProfileUrl = profileUrl
        };

        return View(model);
    }
}