namespace lab10.Models
{
    public class ShopCart
    {
        public List<CartItem> Items { get; set; } = [];

        public decimal TotalAmount => Items.Sum(item => item.TotalPrice);
    }
}