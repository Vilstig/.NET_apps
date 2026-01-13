using System.ComponentModel.DataAnnotations;

namespace lab10.Models
{
    public class OrderViewModel
    {   
        [Required(ErrorMessage = "Proszę podać imię")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwisko")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny format email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Adres jest wymagany")]
        [Display(Name = "Adres i nr domu")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Miasto jest wymagane")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required(ErrorMessage = "Wybierz metodę płatności")]
        [Display(Name = "Metoda płatności")]
        public string PaymentMethod { get; set; }
        
        public List<CartItem> CartItems { get; set; } = [];
        
        public decimal GrandTotal => CartItems.Sum(item => item.TotalPrice);

        public OrderViewModel()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            PaymentMethod = string.Empty;
        }
    }
}