import { Component, inject, Input, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

import { ArticleService } from '../../services/article.service';
import { CategoryService } from '../../services/category.service';
import { Article } from '../../models/article.model';
import { Category } from '../../models/category.model';

@Component({
  selector: 'app-article-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './article-form.html',
  styleUrl: './article-form.scss',
})
export class ArticleForm implements OnInit { 
  
  private articleService = inject(ArticleService);
  private categoryService = inject(CategoryService);
  private router = inject(Router);

  @Input()
  set id(articleId: string | undefined) {
    // Ta funkcja uruchomi się ZA KAŻDYM RAZEM, gdy zmieni się ID w pasku adresu
    if (articleId) {
      this.isEditMode.set(true);
      this.loadArticle(Number(articleId));
    } else {
      // Jeśli ID zniknęło (np. przejście z Edycji do Dodawania), czyścimy formularz
      this.isEditMode.set(false);
      this.resetForm();
    }
  }

  categories = signal<Category[]>([]);
  isEditMode = signal(false);

  newArticle: Article = {
    id: 0,
    name: '',
    price: 0,
    categoryId: 0, 
  };

  resetForm() {
    this.newArticle = {
      id: 0,
      name: '',
      price: 0,
      categoryId: 0,
    };
  }

  ngOnInit(): void {
    this.loadCategories();

    if (this.id) {
      this.isEditMode.set(true);
      this.loadArticle(Number(this.id));
    }
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: (data) => this.categories.set(data),
      error: (err) => console.error('Błąd kategorii:', err)
    });
  }

  loadArticle(id: number) {
    this.articleService.getArticle(id).subscribe({
      next: (data) => {
        this.newArticle = data;
        
        console.log('Dane załadowane do formularza:', this.newArticle);
      },
      error: (err) => console.error('Błąd pobierania artykułu:', err)
    });
  }

  onSave() {
    if (!this.newArticle.name || this.newArticle.price <= 0 || !this.newArticle.categoryId) {
      alert('Wypełnij poprawnie nazwę, cenę i kategorię.');
      return;
    }
    this.newArticle.categoryId = Number(this.newArticle.categoryId);

    if (this.isEditMode()) {
      // EDYCJA
      this.articleService.updateArticle(this.newArticle.id, this.newArticle).subscribe({
        next: () => this.goBack(),
        error: (err) => alert('Błąd zapisu: ' + err.message)
      });
    } else {
      this.articleService.addArticle(this.newArticle).subscribe({
        next: () => this.goBack(),
        error: (err: any) => alert('Błąd tworzenia: ' + err.message)
      });
    }
  }

  goBack() {
    this.router.navigate(['/articles']);
  }
}