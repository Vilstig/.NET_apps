using lab10.Data;
using lab10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ShopController : Controller
{
    private readonly ApplicationDbContext _context;
    private const string PlaceholderImage = "/images/no_image.png";

    public ShopController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Shop/Index
    public async Task<IActionResult> Index(int? categoryId)
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.CurrentCategoryId = categoryId;
        ViewBag.PlaceholderImage = PlaceholderImage;

        IQueryable<Article> articles = _context.Articles.Include(a => a.Category);

        if (categoryId.HasValue)
        {
            articles = articles.Where(a => a.CategoryId == categoryId.Value);
        }

        return View(await articles.ToListAsync());
    }

    // GET: Shop/Details/5 
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var article = await _context.Articles
            .Include(a => a.Category)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (article == null) return NotFound();
        
        ViewBag.PlaceholderImage = PlaceholderImage;
        return View(article);
    }
}