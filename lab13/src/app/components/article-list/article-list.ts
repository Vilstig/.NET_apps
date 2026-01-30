import { Component, input, output, inject, signal } from '@angular/core';
import { Article } from '../../models/article.model';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { ArticleService } from '../../services/article.service';

@Component({
  selector: 'app-article-list',
  imports: [CommonModule, RouterModule],
  templateUrl: './article-list.html',
  styleUrl: './article-list.scss',
})
export class ArticleList {
  private articleService = inject(ArticleService);
  private router = inject(Router);
  protected readonly fileUrl = 'http://localhost:5172/';

  articles = signal<Article[]>([]);
  selectedId = signal<number | null>(null);

  ngOnInit(): void {
    this.loadArticles();
  }

  loadArticles() {
    this.articleService.getArticles().subscribe({
      next: (data) => {
        this.articles.set(data); 
      },
      error: (err) => {
        console.error('Błąd pobierania artykułów:', err);
      }
    });
  }

  selectArticle(id: number) {
    this.selectedId.set(id);
    this.router.navigate(['/articles', id]);
  }

  onAdd() {
    this.router.navigate(['/articles/add']);
  }

  goToDetails(id: number) {
    this.router.navigate(['/articles', id]);
  }
  // 2. Przekierowanie do formularza edycji
  onEdit(id: number) {
    this.router.navigate(['/articles/edit', id]);
  }

  // 3. Usuwanie (zostajemy na liście)
  onDelete(id: number) {
    if (confirm('Czy na pewno chcesz usunąć ten artykuł?')) {
      this.articleService.deleteArticle(id).subscribe({
        next: () => {
          // Po usunięciu odświeżamy listę
          this.loadArticles(); 
        },
        error: (err) => console.error('Błąd usuwania', err)
      });
    }
  }
}
