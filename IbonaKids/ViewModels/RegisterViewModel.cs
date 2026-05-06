using System.ComponentModel.DataAnnotations;

namespace IbonaKids.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Display(Name = "Profile Picture")]
    public IFormFile? ProfilePicture { get; set; }
    public string? ProfileUrl { get; set; }
}
