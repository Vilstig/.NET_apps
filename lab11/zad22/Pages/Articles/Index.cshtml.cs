using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using zad2.Data;
using zad2.Models;

namespace zad2.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Articles != null)
            {
                Article = await _context.Articles
                    .Include(a => a.Category)
                    .ToListAsync();
            }
        }
    }
}