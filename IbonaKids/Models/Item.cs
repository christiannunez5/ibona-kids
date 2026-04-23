using System.ComponentModel.DataAnnotations;

namespace IbonaKids.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        [Required]
        public required string ItemName { get; set; }
        [Required]
        public required string ItemDescription { get; set; }
        [Required]
        public required decimal Price { get; set; }
        [Url]
        public string? ImageUrl { get; set; }

    }
}
