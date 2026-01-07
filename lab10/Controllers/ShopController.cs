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

    public IActionResult AddToCart(int id)
    {
        string cookieKey = "shopCart" + id;

        int currentItems = 0;
        if (Request.Cookies.ContainsKey(cookieKey))
        {
            int.TryParse(Request.Cookies[cookieKey], out currentItems);
        }

        currentItems++;

        CookieOptions option = new()
        {
            Expires = DateTime.Now.AddDays(7)
        };
        Response.Cookies.Append(cookieKey, currentItems.ToString(), option);
        return RedirectToAction("Index");
    }

    public IActionResult Cart()
    {
        ShopCart shopCart = new();
        foreach (var cookie in Request.Cookies)
        {
            if (cookie.Key.StartsWith("shopCart"))
            {
                if (int.TryParse(cookie.Key.Substring("shopCart".Length), out int articleId) &&
                    int.TryParse(cookie.Value, out int quantity))
                {
                    var article = _context.Articles.Find(articleId);
                    if (article != null)
                    {
                        shopCart.Items.Add(new CartItem
                        {
                            Article = article,
                            Quantity = quantity
                        });
                    }
                    else
                    {
                        Response.Cookies.Delete(cookie.Key);
                    }
                }
            }
        }

        ViewBag.PlaceholderImage = PlaceholderImage;
        return View(shopCart);
    }

    public IActionResult ChangeQuantity(int id, int quantity)
    {
        string cookieKey = "shopCart" + id;

        if (quantity <= 0)
        {
            Response.Cookies.Delete(cookieKey);
        }
        else
        {
            CookieOptions option = new()
            {
                Expires = DateTime.Now.AddDays(7)
            };
            Response.Cookies.Append(cookieKey, quantity.ToString(), option);
        }

        return RedirectToAction("Cart");
    }

    public IActionResult Remove(int id)
    {
        string cookieKey = "shopCart" + id;
        Response.Cookies.Delete(cookieKey);
        
        return RedirectToAction("Cart");
    }
}