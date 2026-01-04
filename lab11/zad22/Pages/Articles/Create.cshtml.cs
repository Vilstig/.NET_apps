using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using zad2.Data;
using zad2.Models;

namespace zad2.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private const string PlaceholderImage = "/images/no_image.png";

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Article.Category");

            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
                return Page();
            }

            if (Article.ImageFile != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Article.ImageFile.FileName);
                string path = Path.Combine(wwwRootPath, "images", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await Article.ImageFile.CopyToAsync(fileStream);
                }

                Article.ImageUrl = "/images/" + fileName;
            }
            else
            {
                Article.ImageUrl = PlaceholderImage;
            }

            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}