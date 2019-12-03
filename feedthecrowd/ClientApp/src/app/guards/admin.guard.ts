import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../../services/authentication/authentication.service';

@Injectable({ providedIn: 'root' })
export class AdminGuard implements CanActivate {

  constructor(
    private auth: AuthenticationService,
    private router: Router
  ) { }

  canActivate(): Observable<boolean> | Promise<boolean> | boolean {
    var isAdmin = false;
    this.auth.currentUser.subscribe(d => isAdmin = d.isAdmin);
    if (isAdmin) {
      return true;
    }
    this.router.navigate(['/']);
    return false;
  }

}