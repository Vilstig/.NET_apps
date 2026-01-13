using lab10.Data;
using lab10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public class ShopController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private const string PlaceholderImage = "/images/no_image.png";

    public ShopController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
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

        return View(await articles.OrderBy(a => a.Id).Take(10).ToListAsync());
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

    [Authorize(Policy = "CanShop")]
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

    [Authorize(Policy = "CanShop")]
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

    [Authorize(Policy = "CanShop")]
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

    [Authorize(Policy = "CanShop")]
    public IActionResult Remove(int id)
    {
        string cookieKey = "shopCart" + id;
        Response.Cookies.Delete(cookieKey);

        return RedirectToAction("Cart");
    }

    [Authorize]
    [HttpGet]
    public IActionResult Checkout()
    {
        var model = new OrderViewModel
        {
            CartItems = GetCartItems()
        };

        if (!model.CartItems.Any())
        {
            return RedirectToAction("Index");
        }

        model.Email = User.Identity?.Name;

        return View(model);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ConfirmOrder(OrderViewModel orderData)
    {
        if (ModelState.IsValid)
        {
            var order = new Order
            {
                OrderDate = DateTime.Now,
                UserId = _userManager.GetUserId(User),
                FirstName = orderData.FirstName,
                LastName = orderData.LastName,
                Email = orderData.Email,
                Address = orderData.Address,
                City = orderData.City,
                OrderItems = []
            };

            var cartItems = GetCartItems();

            if (cartItems.Count == 0)
            {
                return RedirectToAction("Index");
            }


            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    ArticleId = item.Article.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.Article.Price
                };

                order.OrderItems.Add(orderItem);
            }

            order.UpdateTotalAmount();

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var cookie in Request.Cookies.Keys)
            {
                if (cookie.StartsWith("shopCart"))
                {
                    Response.Cookies.Delete(cookie);
                }
            }
            orderData.CartItems = cartItems;
            return View("Confirmation", orderData);
        }
        orderData.CartItems = GetCartItems();

        return View("Checkout", orderData);
    }


    private List<CartItem> GetCartItems()
    {
        var items = new List<CartItem>();

        foreach (var cookie in Request.Cookies)
        {
            if (cookie.Key.StartsWith("shopCart"))
            {
                if (int.TryParse(cookie.Key.Substring("shopCart".Length), out int articleId) &&
                    int.TryParse(cookie.Value, out int quantity))
                {
                    var article = _context.Articles.Include(a => a.Category).FirstOrDefault(a => a.Id == articleId);

                    if (article != null)
                    {
                        items.Add(new CartItem
                        {
                            Article = article,
                            Quantity = quantity
                        });
                    }
                }
            }
        }

        return items;
    }
}