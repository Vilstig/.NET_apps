using lab9.Models;

namespace lab9.Data
{
     public class ListArticlesContext : IArticlesContext
    {
        private readonly List<Article> articles = [];
        private int nextId = 1;

        public IEnumerable<Article> GetAll()
        {
            return articles;
        }

        public Article GetById(int id)
        {
            return articles.FirstOrDefault(a => a.Id == id);
        }

        public void Add(Article article)
        {
            article.Id = nextId++;
            articles.Add(article);
        }

        public void Update(Article article)
        {
            var existing = GetById(article.Id);
            if (existing != null)
            {
                existing.Name = article.Name;
                existing.Price = article.Price;
                existing.ExpirationDate = article.ExpirationDate;
                existing.Category = article.Category;
            }
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item != null)
                articles.Remove(item);
        }
    }
}