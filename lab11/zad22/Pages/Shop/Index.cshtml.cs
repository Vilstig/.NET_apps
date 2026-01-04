using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using zad2.Data;
using zad2.Models;

namespace zad2.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        
        public const string PlaceholderImage = "/images/no_image.png";

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Category> Categories { get; set; } = default!;
        public IList<Article> Articles { get; set; } = default!;
        
        public int? CurrentCategoryId { get; set; }

        public async Task OnGetAsync(int? categoryId)
        {
            CurrentCategoryId = categoryId;

            Categories = await _context.Categories.ToListAsync();

            IQueryable<Article> articlesQuery = _context.Articles.Include(a => a.Category);

            if (categoryId.HasValue)
            {
                articlesQuery = articlesQuery.Where(a => a.CategoryId == categoryId.Value);
            }

            Articles = await articlesQuery.ToListAsync();
        }
    }
}