import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Product } from '../../app/models/product';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private readonly productsApi = `${environment.webApiUrl}/products`;
  constructor(private http: HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.productsApi);
  }
  getProduct(productId: string): Observable<Product> {
    return this.http.get<Product>(`${this.productsApi}/${productId}`);
  }
  getProductsByRecipe(recipeId: string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.productsApi}/${recipeId}/recipe`);
  }
  createProducts(recipeId: string, product: Product[]): Observable<Product[]> {
    return this.http.post<Product[]>(`${this.productsApi}/${recipeId}`, product);
  }
}

