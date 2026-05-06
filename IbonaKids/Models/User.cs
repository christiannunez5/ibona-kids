namespace IbonaKids.Models;

public enum UserRole
{
    Admin,
    User
}

public class User
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public string? Username { get; set; }
    public required string Password { get; set; }
    public UserRole Roles { get; set; }
    public double Balance { get; set; } = 10000.00;
    public string? ProfileUrl { get; set; }
}
