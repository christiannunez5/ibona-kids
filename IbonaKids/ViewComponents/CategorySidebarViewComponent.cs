using IbonaKids.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IbonaKids.ViewComponents;

public class CategorySidebarViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public CategorySidebarViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _context.ProductCategories
            .OrderBy(c => c.Name)
            .ToListAsync();

        return View(categories);
    }
}