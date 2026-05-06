using Microsoft.EntityFrameworkCore;

namespace IbonaKids.Models;

public class ProductCategory
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }

    [Precision(18, 2)]
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;
    public int StockQuantity { get; set; } = 50;
    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public int CreatedById { get; set; }
    public int ProductCategoryId { get; set; }

    public User CreatedBy { get; set; } = null!;
    public ProductCategory ProductCategory { get; set; } = null!;
}