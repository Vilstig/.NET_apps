using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using zad2.Data;
using zad2.Models;

namespace zad2.Pages.Articles
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private const string PlaceholderImage = "/images/no_image.png";

        public DeleteModel(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var article = await _context.Articles.FindAsync(id);

            if (article != null)
            {
                if (!string.IsNullOrEmpty(article.ImageUrl) && article.ImageUrl != PlaceholderImage)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string filePath = Path.Combine(wwwRootPath, article.ImageUrl.TrimStart('/'));

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}