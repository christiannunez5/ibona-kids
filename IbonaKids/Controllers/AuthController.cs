using IbonaKids.Data;
using IbonaKids.Models;
using IbonaKids.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IbonaKids.Controllers;

public class AuthController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AuthController(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
    {
        _dbContext = dbContext;
        _webHostEnvironment = webHostEnvironment;
    }

    #region Login

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _dbContext.Users.FirstOrDefault(u =>
            u.Email == model.Email && u.Password == model.Password);

        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Username ?? user.Email),
            new(ClaimTypes.Role, user.Roles.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
             CookieAuthenticationDefaults.AuthenticationScheme,
             principal);

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        // redirect user based on roles
        return user.Roles == UserRole.Admin
            ? RedirectToAction("Dashboard", "Admin")
            : RedirectToAction("Index", "Home");
    }

    #endregion

    #region Register

    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var emailExists = _dbContext.Users.Any(u => u.Email == model.Email);
        if (emailExists)
        {
            ModelState.AddModelError("Email", "Email is already taken.");
            return View(model);
        }

        var usernameExists = _dbContext.Users.Any(u => u.Username == model.Username);
        if (usernameExists)
        {
            ModelState.AddModelError("Username", "Username is already taken.");
            return View(model);
        }

        string? profileUrl = null;
        if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var extension = Path.GetExtension(model.ProfilePicture.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("ProfilePicture", "Only image files are allowed (.jpg, .jpeg, .png, .gif, .webp).");
                return View(model);
            }

            if (model.ProfilePicture.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("ProfilePicture", "Image size must not exceed 2MB.");
                return View(model);
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "avatars");
            Directory.CreateDirectory(uploadsFolder);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await model.ProfilePicture.CopyToAsync(stream);

            profileUrl = $"/uploads/avatars/{fileName}";
        }

        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            Password = model.Password,
            Roles = UserRole.User,
            ProfileUrl = profileUrl
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction("Login");
    }

    #endregion


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
