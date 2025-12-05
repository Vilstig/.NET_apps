using System.ComponentModel.DataAnnotations;

namespace lab9.Models
{
    public enum ArticleCategory
    {
        Food,
        Electronics,
        Other
    }

    public class Article
    {
        
        public Article() { }

        // Opcjonalny konstruktor z parametrami
        public Article(int id, string name, decimal price, DateTime expirationDate, ArticleCategory category)
        {
            Id = id;
            Name = name;
            Price = price;
            ExpirationDate = expirationDate;
            Category = category;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, 99999)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Expiration Date")]
        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }  

        [Required]
        public ArticleCategory Category { get; set; }
    }
}