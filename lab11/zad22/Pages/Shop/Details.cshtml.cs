using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using zad2.Data;
using zad2.Models;

namespace zad2.Pages.Shop
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public const string PlaceholderImage = "/images/no_image.png";

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null) return NotFound();

            Article = article;
            return Page();
        }
    }
}