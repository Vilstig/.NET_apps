import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleService } from '../../services/article.service';
import { Article } from '../../models/article.model';

@Component({
  selector: 'app-article-details',
  imports: [CommonModule],
  templateUrl: './article-details.html',
  styleUrl: './article-details.scss',
})
export class ArticleDetails {
  private route = inject(ActivatedRoute);
  private articleService = inject(ArticleService);
  protected readonly fileUrl = 'http://localhost:5172/';

  article = signal<Article | undefined>(undefined);

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const id = Number(params.get('id'));
      if (id) {
        this.articleService.getArticle(id).subscribe({
          next: (data) => this.article.set(data),
          error: (error) => console.error('Error fetching article:', error)
        });
      }
    });  
  }
}
