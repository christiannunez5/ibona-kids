using System.ComponentModel.DataAnnotations;

namespace IbonaKids.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }
        [Required]
        public required string ItemName { get; set; }
        [Required]
        public required string ItemDescription { get; set; }
        [Required]
        public required decimal ItemPrice { get; set; }
        [Url]
        public string? ItemImageUrl { get; set; }

    }
}
