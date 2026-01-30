import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Potrzebne do [(ngModel)]
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/category.model';

@Component({
  selector: 'app-category-manager',
  imports: [CommonModule, FormsModule],
  templateUrl: './category-manager.html',
  styleUrl: './category-manager.scss',
})
export class CategoryManager implements OnInit {
  private categoryService = inject(CategoryService);

  categories = signal<Category[]>([]);
  isEditing = signal<boolean>(false);

  categoryName = '';
  editingId: number | null = null;

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe((data) => {
      this.categories.set(data);
    });
  }

  saveCategory() {
    if (!this.categoryName.trim()) return;

    if (this.isEditing() && this.editingId) {
      const updatedCat: Category = { id: this.editingId, name: this.categoryName };
      this.categoryService.updateCategory(this.editingId, updatedCat).subscribe(() => {
        this.loadCategories();
        this.cancelEdit();
      });
    } else {
      const newCat: Partial<Category> = { name: this.categoryName };
      this.categoryService.addCategory(newCat).subscribe(() => {
        this.loadCategories();
        this.categoryName = '';
      });
    }
  }

  deleteCategory(id: number) {
    if (confirm('Czy na pewno chcesz usunąć tę kategorię?')) {
      this.categoryService.deleteCategory(id).subscribe(() => {
        this.loadCategories();
      });
    }
  }

  startEdit(cat: Category) {
    this.isEditing.set(true);
    this.categoryName = cat.name;
    this.editingId = cat.id;
  }

  cancelEdit() {
    this.isEditing.set(false);
    this.categoryName = '';
    this.editingId = null;
  }
}
