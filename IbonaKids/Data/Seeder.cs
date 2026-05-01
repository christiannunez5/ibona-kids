using IbonaKids.Models;
using Microsoft.EntityFrameworkCore;

namespace IbonaKids.Data;

public static class Seeder
{
    public async static Task SeedData(AppDbContext dbContext)
    {
        if (!await dbContext.Users.AnyAsync())
        {
            var newAdmin = new User
            {
                Email = "admin@gmail.com",
                Password = "admin12345",
                Roles = UserRole.Admin,
                Username = "admin1",
            };

            await dbContext.Users.AddAsync(newAdmin);
            await dbContext.SaveChangesAsync();
        }
    }
}
