using Microsoft.EntityFrameworkCore;

namespace IbonaKids.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}
