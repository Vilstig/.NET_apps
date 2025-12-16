using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace lab10.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; } = new List<Article>();
    
        public Category()
        {
            Name = string.Empty;
        }

        public Category(string name)
        {
            Name = name;
        }
    }
}