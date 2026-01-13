// Article.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using System.Text.Json.Serialization;

namespace lab10.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole Nazwa jest wymagane.")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole Cena jest wymagane.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }


        [Required(ErrorMessage = "Wybór kategorii jest obowiązkowy.")]
        public int CategoryId { get; set; }
       
        [JsonIgnore]
        public Category? Category { get; set; } = default!;

        [NotMapped]
        [JsonIgnore]
        public IFormFile? ImageFile { get; set; }
    }
}