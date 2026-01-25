import { Component, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Article, ArticleCategory } from '../../models/article.model';
@Component({
  selector: 'app-article-form',
  imports: [FormsModule],
  templateUrl: './article-form.html',
  styleUrl: './article-form.scss',
})
export class ArticleForm {
  categories = input.required<ArticleCategory[]>();

  save = output<Article>();
  cancel = output<void>();

  newArticle: Article = {
    id: 0,
    name: '',
    price: 0,
    category: 'Technology',
    image: null
  }

  onSave() {
    if (this.newArticle.name.trim() && this.newArticle.price > 0) {
      this.save.emit(this.newArticle);
    } else {
      alert('Proszę wypełnić wszystkie pola poprawnie.');
    }
  }
}
