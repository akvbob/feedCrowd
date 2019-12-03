import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Recipe } from '../../app/models/recipe';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Event } from '../../app/models/event';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private readonly eventsApi = `${environment.webApiUrl}/events`;
  constructor(private http: HttpClient) { }
  create(event: Event) {
    return this.http.post(`${this.eventsApi}`, event);
  }
  delete(id: string) {
    return this.http.delete(`${this.eventsApi}/${id}`);
  }
  getUserEvents(userId: string): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.eventsApi}/user/${userId}`);
  }
  getEvent(id: string): Observable<Event> {
    return this.http.get<Event>(`${this.eventsApi}/${id}`);
  }
  update(id: string, event: Event) {
    console.log('ATEJAU'); console.log(`${this.eventsApi}/${id}`);
    return this.http.put(`${this.eventsApi}/${id}`, event);
  }
  addRecipeToEvent(recipeId: string, eventId: string) {
    return this.http.post(`${this.eventsApi}/recipe/${recipeId}/${eventId}`, {});
  }
  removeRecipeFromEvent(recipeId: string, eventId: string) {
    return this.http.delete(`${this.eventsApi}/recipe/${recipeId}/${eventId}`);
  }
}
