using IbonaKids.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IbonaKids.Controllers;

public class ProductsController : Controller
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? productCategoryId)
    {
        var query = _context.Products
            .Include(p => p.ProductCategory)
            .Where(p => p.IsActive);

        if (productCategoryId is not null)
        {
            query = query.Where(p => p.ProductCategoryId == productCategoryId.Value);
        }

        var products = await query
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        if (productCategoryId.HasValue)
        {
            var category = await _context.ProductCategories.FindAsync(productCategoryId.Value);
            ViewBag.SelectedCategory = category?.Name;
        }
        else
        {
            ViewBag.SelectedCategory = "All Products";
        }

        return View(products);
    }
}
