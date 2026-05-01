using Microsoft.EntityFrameworkCore;

namespace IbonaKids.Models
{
    public enum OrderStatus
    {
        Pending,
        Paid,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }

        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public int UserId { get; set; }

        public User User { get; set; } = null!;
        //public ICollection<Item> OrderItems { get; set; } = new List<Item>();
    }
}
