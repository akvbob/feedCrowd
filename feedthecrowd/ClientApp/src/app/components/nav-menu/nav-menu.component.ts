import { Component } from '@angular/core';
import { User } from 'src/app/models/user';
import { AuthenticationService } from 'src/services/authentication/authentication.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
const Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 2500
});
@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  user: User;
  localUser: any;
  id: any;
  
  navbarOpen = false;

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router) {}

    toggleNavbar() {
      this.navbarOpen = !this.navbarOpen;
    }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  isLoggedIn() {
    if (sessionStorage.getItem('currentUser')) {
      this.localUser = JSON.parse(sessionStorage.getItem('currentUser'));
      this.id = this.localUser.id;
      return true;
    }
    return false;
  }
  isAdmin(){
    if(sessionStorage.getItem('currentUser')){
      return JSON.parse(sessionStorage.getItem('currentUser')).isAdmin;
    }
    return false;
  }
  logout() {
    this.authenticationService.logout();
    if (!this.isLoggedIn()) {
      Toast.fire({
        type: 'success',
        title: 'Logout successful'
      });
      this.router.navigate(['/']);
    }
  }
}
