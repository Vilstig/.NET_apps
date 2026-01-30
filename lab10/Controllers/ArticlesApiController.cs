using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab10.Data;
using lab10.Models;
using Microsoft.AspNetCore.Cors;

namespace lab10.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/article")] // Adres będzie: /api/article
    [ApiController]
    public class ArticlesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArticlesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/article?skip=0&take=4&categoryId=1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles(
            [FromQuery] int skip = 0, 
            [FromQuery] int take = 10, 
            [FromQuery] int? categoryId = null)
        {
            var query = _context.Articles.Include(a => a.Category).AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(a => a.CategoryId == categoryId);
            }

            // Sortowanie jest ważne przy stronicowaniu!
            var articles = await query
                .OrderBy(a => a.Id) 
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return Ok(articles);
        }

        // GET: api/article/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // POST: api/article
        [HttpPost]
        
        public async Task<ActionResult<Article>> PostArticle([FromBody] Article article)
        {
            if (string.IsNullOrEmpty(article.ImageUrl))
            {
                article.ImageUrl = "/images/no_image.png"; 
            }

            if (!_context.Categories.Any(c => c.Id == article.CategoryId))
            {
                 return BadRequest("Podana kategoria nie istnieje.");
            }

            article.Category = null; 

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.Id }, article);
        }

        // PUT: api/article/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, [FromBody]Article article)
        {
            if (id != article.Id)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Articles.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/article/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}