import { Routes } from '@angular/router';
import { ArticleList} from './components/article-list/article-list';
import { ArticleForm } from './components/article-form/article-form';
import { ArticleDetails } from './components/article-details/article-details';
import { CategoryManager } from './components/category-manager/category-manager'; // Import

export const routes: Routes = [
  { path: '', redirectTo: 'articles', pathMatch: 'full' }, 

  // 1. Najpierw konkretne ścieżki (WAŻNE: 'add' musi być przed ':id')
  { path: 'articles/add', component: ArticleForm }, 
  
  // 2. Edycja
  { path: 'articles/edit/:id', component: ArticleForm },

  // 3. Szczegóły (To ma ':id', więc łapie wszystko co nie jest 'add' ani 'edit')
  { path: 'articles/:id', component: ArticleDetails },

  // 4. Lista artykułów (baza)
  { path: 'articles', component: ArticleList },

  // 5. Kategorie
  { path: 'categories', component: CategoryManager },
];