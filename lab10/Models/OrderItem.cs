using System.ComponentModel.DataAnnotations;

namespace lab10.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; } = default!;
        [Required]
        public int ArticleId { get; set; }

        public Article Article { get; set; } = default!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;
    }
}