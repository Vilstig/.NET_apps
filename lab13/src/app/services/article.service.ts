import { Injectable } from '@angular/core';
import { Article, ArticleCategory } from '../models/article.model';

@Injectable({
  providedIn: 'root',
})
export class ArticleService {
  categories: ArticleCategory[] = ['Technology', 'Health', 'Finance', 'Education', 'Entertainment', 'Food'];
  
  articles: Article[] = [
    { id: 1, name: 'Mleko', price: 3.50, image: null, category: 'Food' },
    { id: 2, name: 'Szampon', price: 15.99, image: null, category: 'Health' },
    { id: 3, name: 'Słuchawki', price: 150.00, image: null, category: 'Technology' }];

    constructor() { }

    getArticles(): Article[] {
      return this.articles;
    }

    getCategories(): ArticleCategory[] {
      return this.categories;
    }

    addArticle(article: Article): void {
      this.articles.push(article);
    }

    getArticleById(id: number): Article | undefined {
      return this.articles.find(article => article.id === id);
    }

    deleteArticle(id: number): void {
      this.articles = this.articles.filter(a => a.id !== id);
    }
}
