import { Component, input, output } from '@angular/core';
import { Article } from '../../models/article.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-article-list',
  imports: [CommonModule],
  templateUrl: './article-list.html',
  styleUrl: './article-list.scss',
})
export class ArticleList {
  articles = input.required<Article[]>();
  remove = output<number>()
  createNew = output<void>();
}
