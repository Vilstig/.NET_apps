export type ArticleCategory = 'Technology' | 'Health' | 'Finance' | 'Education' | 'Entertainment' | 'Food';

export interface Article {
    id: number;
    name: string;
    price: number;
    image?: string | null;
    category: ArticleCategory;
}