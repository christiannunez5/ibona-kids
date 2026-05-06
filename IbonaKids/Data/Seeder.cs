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

        if (!await dbContext.ProductCategories.AnyAsync())
        {
            var categories = new List<ProductCategory>
            {
                new ProductCategory { Name = "Mouse" },
                new ProductCategory { Name = "Keyboard" },
                new ProductCategory { Name = "Headset" },
                new ProductCategory { Name = "Monitor" },
                new ProductCategory { Name = "Speakers" },
            };
            await dbContext.ProductCategories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();
        }

        if (!await dbContext.Products.AnyAsync())
        {
            var admin = await dbContext.Users.FirstAsync();
            var mouse = await dbContext.ProductCategories.FirstAsync(c => c.Name == "Mouse");
            var keyboard = await dbContext.ProductCategories.FirstAsync(c => c.Name == "Keyboard");
            var headset = await dbContext.ProductCategories.FirstAsync(c => c.Name == "Headset");
            var monitor = await dbContext.ProductCategories.FirstAsync(c => c.Name == "Monitor");
            var speakers = await dbContext.ProductCategories.FirstAsync(c => c.Name == "Speakers");

            var products = new List<Product>
            {
                // Mouse
                new Product {
                    Name = "Zyrex Phantom X1",
                    Price = 49.99m,
                    ProductCategoryId = mouse.Id,
                    CreatedById = admin.Id,
                    IsActive = true,
                    StockQuantity = 50,
                    Description = "High-precision optical gaming mouse with 25K DPI sensor",
                    ImageUrl = "https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=400&q=80"
                },
                new Product {
                    Name = "Voltex GlideX Pro",
                    Price = 69.99m,
                    ProductCategoryId = mouse.Id,
                    CreatedById = admin.Id,
                    IsActive = true,
                    StockQuantity = 40,
                    Description = "Ergonomic wireless gaming mouse with ultra-light shell",
                    ImageUrl = "https://images.unsplash.com/photo-1629429408209-1f912961dbd8?w=400&q=80"
                },
                new Product {
                    Name = "Nexon Swipe 600",
                    Price = 39.99m,
                    ProductCategoryId = mouse.Id,
                    CreatedById = admin.Id,
                    IsActive = true,
                    StockQuantity = 35,
                    Description = "Dual sensor gaming mouse with customizable RGB lighting",
                    ImageUrl = "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?w=400&q=80"
                },

                // Keyboard
                new Product {
                    Name = "Strikex K900 RGB",
                    Price = 129.99m,
                    ProductCategoryId = keyboard.Id,
                    CreatedById = admin.Id,
                    IsActive = true,
                    StockQuantity = 20,
                    Description = "Full-size mechanical keyboard with tactile switches and per-key RGB",
                    ImageUrl = "https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=400&q=80"
                },
                new Product {
                    Name = "Pulsar TKL-80",
                    Price = 89.99m,
                    ProductCategoryId = keyboard.Id,
                    CreatedById = admin.Id,
                    IsActive = true,
                    StockQuantity = 30,
                    Description = "Compact tenkeyless mechanical keyboard with RGB backlight",
                    ImageUrl = "https://images.unsplash.com/photo-1541140532154-b024d705b90a?w=400&q=80"
                },
                new Product {
                    Name = "Kronos K2 Wireless",
                    Price = 99.99m,
                    ProductCategoryId = keyboard.Id,
                    CreatedById = admin.Id,
                    IsActive = true,
                    StockQuantity = 25,
                    Description = "Wireless mechanical keyboard with hot-swap switch support",
                    ImageUrl = "https://images.unsplash.com/photo-1618384887929-16ec33fab9ef?w=400&q=80"
                },

                // Headset
                new Product {
                    Name = "Aurox CloudX II",
                    Price = 74.99m,
                    ProductCategoryId = headset.Id,
                    CreatedById = admin.Id,
                    IsActive = true,
                    StockQuantity = 40,
                    Description = "7.1 virtual surround sound gaming headset with memory foam",
                    ImageUrl = "https://images.unsplash.com/photo-1618366712010-f4ae9c647dcb?w=400&q=80"
                },
                new Product {
                    Name = "Vortex Arctis W7",
                    Price = 99.99m,
                    ProductCategoryId = headset.Id,
                    CreatedById = admin.Id,
                    IsActive = true,
                    StockQuantity = 35,
                    Description = "Wireless lossless gaming headset with 24hr battery life",
                    ImageUrl = "https://images.unsplash.com/photo-1583394838336-acd977736f90?w=400&q=80"
                },
                new Product {
                    Name = "Eclipx ShadowSound V2",
                    Price = 79.99m,
                    ProductCategoryId = headset.Id,
                    CreatedById = admin.Id,
                    IsActive = true,
                    StockQuantity = 30,
                    Description = "Esports headset with spatial audio and noise-cancelling mic",
                    ImageUrl = "https://cdn.thewirecutter.com/wp-content/media/2025/11/BEST-GAMING-HEADSETS-0049.jpg"
                },

                // Monitor
                new Product {
                    Name = "Lumex UltraView 27",
                    Price = 299.99m,
                    ProductCategoryId = monitor.Id,
                    CreatedById = admin.Id,
                    IsActive = true,
                    StockQuantity = 15,
                    Description = "27-inch 1440p 165Hz IPS gaming monitor with G-Sync",
                    ImageUrl = "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=400&q=80"
                },
                new Product {
                    Name = "Prizma Curve G5",
                    Price = 249.99m,
                    ProductCategoryId = monitor.Id,
                    CreatedById = admin.Id,
                    IsActive = true,
                    StockQuantity = 12,
                    Description = "32-inch curved VA panel gaming monitor at 144Hz",
                    ImageUrl = "https://images.unsplash.com/photo-1593640495253-23196b27a87f?w=400&q=80"
                },

                // Speakers
                new Product {
                    Name = "Bassix Z400 BT",
                    Price = 79.99m,
                    ProductCategoryId = speakers.Id,
                    CreatedById = admin.Id,
                    IsActive = true,
                    StockQuantity = 20,
                    Description = "Bluetooth 2.1 desktop speakers with deep subwoofer",
                    ImageUrl = "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=400&q=80"
                },
                new Product {
                    Name = "Nomvex RiftSound V2",
                    Price = 149.99m,
                    ProductCategoryId = speakers.Id,
                    CreatedById = admin.Id,
                    IsActive = true,
                    StockQuantity = 18,
                    Description = "Full-range stereo gaming speakers with RGB ambient lighting",
                    ImageUrl = "https://images.unsplash.com/photo-1545454675-3531b543be5d?w=400&q=80"
                },
            };

            await dbContext.Products.AddRangeAsync(products);
            await dbContext.SaveChangesAsync();
        }
    }
}