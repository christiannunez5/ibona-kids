using System.ComponentModel.DataAnnotations;

namespace IbonaKids.ViewModels
{
    public class CartItemViewModel
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}