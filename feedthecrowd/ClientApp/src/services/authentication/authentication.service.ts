import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

import { User } from '../../app/models/user';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private readonly authApi = `${environment.webApiUrl}/auth`;
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(sessionStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        console.log("current user VALUEEEEEEEEEEEE");
        console.log(this.currentUserSubject.value);
        return this.currentUserSubject.value;
    }

    login(username: string, password: string) {
        return this.http.post<any>(`${this.authApi}/login`, { username, password })
            .pipe(map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('token', user.token);
                    localStorage.setItem('user', JSON.stringify(user));
                    sessionStorage.setItem('currentUser', JSON.stringify(user));
                    console.log("PRISILOGINO + tokens:" + user.token);
                    this.currentUserSubject.next(user);
                }

                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        sessionStorage.removeItem('currentUser');
        sessionStorage.clear();
        this.currentUserSubject.next(null);
    }
}
