namespace lab10.Models
{
    public class CartItem
    {
        public Article Article { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal TotalPrice => Article.Price * Quantity;
    }
}