import { Category } from "./category.model";

export interface Article {
  id: number;
  name: string;
  price: number;
  imageUrl?: string; 
  
  categoryId: number;
}