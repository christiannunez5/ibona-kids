using System.Security.Claims;
using IbonaKids.Data;
using IbonaKids.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IbonaKids.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId)
        {
            if (User.Identity?.IsAuthenticated != true)
            {
                var returnUrl = Request.Headers["Referer"].ToString() ?? Url.Action("Index", "Home");
                return RedirectToAction("Login", "Auth", new { returnUrl });
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null || product.StockQuantity <= 0)
                return Redirect(Request.Headers["Referer"].ToString() ?? Url.Action("Index", "Home"));

            var existing = await _context.Carts.FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);
            if (existing != null)
            {
                existing.Quantity += 1;
                _context.Carts.Update(existing);
            }
            else
            {
                var cart = new Cart
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = 1,
                    AddedAt = DateTime.UtcNow
                };
                await _context.Carts.AddAsync(cart);
            }

            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString() ?? Url.Action("Index", "Home"));
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, int quantity)
        {
            if (User.Identity?.IsAuthenticated != true)
                return RedirectToAction("Login", "Auth", new { returnUrl = Url.Action("Index", "Checkout") });

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var item = await _context.Carts.Include(c => c.Product).FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (item == null)
                return RedirectToAction("Index", "Checkout");

            if (quantity <= 0)
            {
                _context.Carts.Remove(item);
            }
            else
            {
                item.Quantity = Math.Min(quantity, item.Product?.StockQuantity ?? quantity);
                _context.Carts.Update(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Checkout");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            if (User.Identity?.IsAuthenticated != true)
                return RedirectToAction("Login", "Auth", new { returnUrl = Url.Action("Index", "Checkout") });

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var item = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (item != null)
            {
                _context.Carts.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Checkout");
        }
    }
}