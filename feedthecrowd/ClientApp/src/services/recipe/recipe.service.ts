import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Recipe } from '../../app/models/recipe';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  private readonly recipesApi = `${environment.webApiUrl}/recipes`;
  constructor(private http: HttpClient) { }

  getRecipes(): Observable<Recipe[]> {
    return this.http.get<Recipe[]>(this.recipesApi);
  }
  getOwnRecipes(userId: string): Observable<Recipe[]> {
    return this.http.get<Recipe[]>(`${this.recipesApi}/user/${userId}`);
  }
  getRecipe(recipeId: string): Observable<Recipe> {
    return this.http.get<Recipe>(`${this.recipesApi}/${recipeId}`);
  }
  update(recipeId: string, recipe: Recipe) {
    return this.http.put(`${this.recipesApi}/${recipeId}`, recipe);
  }
  create(recipe: Recipe) {
    console.log(this.http.post(`${this.recipesApi}`, recipe));
    return this.http.post(`${this.recipesApi}`, recipe);
  }
  getByEvent(eventId: string): Observable<Recipe[]> {
    return this.http.get<Recipe[]>(`${this.recipesApi}/event/${eventId}`);
  }
  delete(recipeId: string) {
    return this.http.delete(`${this.recipesApi}/${recipeId}`);
  }
}
