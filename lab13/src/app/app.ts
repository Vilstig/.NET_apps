import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArticleService } from './services/article.service';
import { Article, ArticleCategory } from './models/article.model';
import { ArticleList } from './components/article-list/article-list';
import { ArticleForm } from './components/article-form/article-form';
import { FooterComponent } from './components/footer/footer';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, ArticleList, ArticleForm, FooterComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements OnInit {
  private articleService = inject(ArticleService);

  articles: Article[] = [];
  categories: ArticleCategory[] = [];
  isFormVisible = false;

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    // Uwaga: W prawdziwej aplikacji użylibyśmy Signals w serwisie,
    // tutaj odświeżamy referencję tablicy ręcznie po zmianach.
    this.articles = [...this.articleService.getArticles()];
    this.categories = this.articleService.getCategories();
  }

  showForm() {
    this.isFormVisible = true;
  }

  hideForm() {
    this.isFormVisible = false;
  }

  handleRemove(id: number) {
    this.articleService.deleteArticle(id);
    this.loadData(); // Odśwież widok
  }

  handleSave(newArticle: Article) {
    // Generowanie prostego ID (w realnej aplikacji robi to backend)
    newArticle.id = Math.max(...this.articles.map(a => a.id), 0) + 1;
    
    this.articleService.addArticle(newArticle);
    this.loadData(); // Odśwież dane
    this.hideForm(); // Wróć do listy
  }
}