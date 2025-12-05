using lab9.Models;

namespace lab9.Data
{
    public class DictionaryArticlesContext : IArticlesContext
    {
        private readonly Dictionary<int, Article> articles = [];
        private int nextId = 1;
        public IEnumerable<Article> GetAll()
        {
            return articles.Values;
        }

        public Article GetById(int id)
        {
            articles.TryGetValue(id, out var article);
            return article;
        }

        public void Add(Article article)
        {
            article.Id = nextId++;
            articles[article.Id] = article;
        }

        public void Update(Article article)
        {
            if (articles.ContainsKey(article.Id))
            {
                articles[article.Id] = article;
            }
        }

        public void Delete(int id)
        {
            articles.Remove(id);
        }
    }
}