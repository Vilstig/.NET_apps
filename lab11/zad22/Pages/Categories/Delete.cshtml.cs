using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using zad2.Data;
using zad2.Models;

namespace zad2.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories
                .Include(c => c.Articles) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null) return NotFound();

            Category = category;

            if (Category.Articles != null && Category.Articles.Count > 0)
            {
                ErrorMessage = $"Nie można usunąć kategorii '{Category.Name}', ponieważ ma przypisane {Category.Articles.Count} towarów. Usuń najpierw towary.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories.FindAsync(id);

            if (category == null) return NotFound();

            bool hasArticles = await _context.Articles.AnyAsync(a => a.CategoryId == id);

            if (hasArticles)
            {
                Category = category;
                ErrorMessage = "Nie można usunąć tej kategorii, ponieważ posiada przypisane towary.";
                return Page();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}