import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Product } from '../../app/models/product';
import { Observable } from 'rxjs';
import { NewComment } from 'src/app/models/newComment';
@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private readonly commentsApi = `${environment.webApiUrl}/comments`;
  constructor(private http: HttpClient) { }
  
  getCommentsOfRecipe(recipeId: string): Observable<Comment[]> {
    return this.http.get<Comment[]>(`${this.commentsApi}/${recipeId}/recipe`);
  }
  createComment(comment: NewComment){
      return this.http.post<NewComment>(`${this.commentsApi}`, comment);
  }
  delete(id: number) {
    return this.http.delete(this.commentsApi+ '/' + id);
  }
  /*getProducts(): Observable<Comment[]> {
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
  }*/
}

