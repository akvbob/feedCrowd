import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {User} from '../../app/models/user';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly authApi = `${environment.webApiUrl}/auth`;
  private readonly api = `${environment.webApiUrl}`;

  constructor(private http: HttpClient) { }

 getAll(): Observable<User[]>  {console.log("einu prasyt");
      return this.http.get<User[]>(this.api + `/users`);
  }

  getById(id: number) {
      return this.http.get(this.api + `/users/` + id);
  }

  register(user: User) {
      return this.http.post(this.authApi + '/register', user);
  }

  update(user: User) {
      return this.http.put(this.api + `/users/` + user.id, user);
  }

  delete(id: number) {
      return this.http.delete(this.api + `/users/` + id);
  }
}
