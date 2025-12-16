using lab10.Data;
using lab10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class ArticleController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _hostEnvironment; 
    private const string PlaceholderImage = "/images/no_image.png";

    public ArticleController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
    }


    public async Task<IActionResult> Index()
    {
        var articles = _context.Articles.Include(a => a.Category);
        return View(await articles.ToListAsync());
    }

    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Article article)
    {
        ModelState.Remove(nameof(Article.Category));

        if (ModelState.IsValid)
        {
            if (article.ImageFile != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(article.ImageFile.FileName);
                string path = Path.Combine(wwwRootPath, "images", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await article.ImageFile.CopyToAsync(fileStream);
                }
                
                article.ImageUrl = "/images/" + fileName;
            }
            else
            {
                article.ImageUrl = PlaceholderImage;
            }

            _context.Add(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
        return View(article);
    }
    
    public async Task<IActionResult> Details(int? id)
    {
        return View(await GetArticleById(id));
    }
    
    public async Task<IActionResult> Edit(int? id)
    {
        var article = await GetArticleById(id);
        if (article == null) return NotFound();
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
        return View(article);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ImageUrl,CategoryId")] Article article)
    {
        if (id != article.Id) return NotFound();
        ModelState.Remove(nameof(Article.Category)); 
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(article);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(article.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
        return View(article);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        return View(await GetArticleById(id));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
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

        return RedirectToAction(nameof(Index));
    }
    
    private async Task<Article?> GetArticleById(int? id)
    {
        if (id == null) return null;
        return await _context.Articles
            .Include(a => a.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    private bool ArticleExists(int id)
    {
        return _context.Articles.Any(e => e.Id == id);
    }
}