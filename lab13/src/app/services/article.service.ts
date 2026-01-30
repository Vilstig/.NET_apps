import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http'; // Dodaj HttpParams
import { Observable } from 'rxjs';
import { Article } from '../models/article.model';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5172/api/article'; 


  getArticles(skip: number = 0, take: number = 100, categoryId?: number): Observable<Article[]> {
    let params = new HttpParams()
      .set('skip', skip)
      .set('take', take);

    if (categoryId) {
      params = params.set('categoryId', categoryId);
    }

    return this.http.get<Article[]>(this.apiUrl, { params });
  }

  getArticle(id: number): Observable<Article> {
    return this.http.get<Article>(`${this.apiUrl}/${id}`);
  }

  addArticle(article: Article): Observable<Article> {
    return this.http.post<Article>(this.apiUrl, article);
  }

  updateArticle(id: number, article: Article): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, article);
  }

  deleteArticle(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}