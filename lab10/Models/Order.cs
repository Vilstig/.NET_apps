using System.ComponentModel.DataAnnotations; 
using Microsoft.AspNetCore.Identity;

namespace lab10.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        
        [Required]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public decimal TotalAmount { get; set;}

        [Required]
        [StringLength(50)] 
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }
        public List<OrderItem> OrderItems { get; set; } = [];

        public Order()
        {
            UserId = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            City = string.Empty;
        }

        public void UpdateTotalAmount()
        {
            
            TotalAmount = OrderItems.Sum(item => item.TotalPrice);
        }
    }
}