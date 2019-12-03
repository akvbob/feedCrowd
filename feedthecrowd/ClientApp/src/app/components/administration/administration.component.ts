import { Component } from '@angular/core';
import { UserService } from 'src/services/user/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { User } from 'src/app/models/user';
import Swal from 'sweetalert2';
const Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 2500
});
@Component({
  selector: 'app-administration',
  templateUrl: './administration.component.html',
  styleUrls: ['./administration.component.css']
})

export class AdministrationComponent {

  users: User[];
  constructor(private userService: UserService, private router: Router, private route: ActivatedRoute) { }
  ngOnInit() {
    this.userService.getAll().subscribe(users => {
      this.users = users;
    }, error => { }
    );
  }

  changeRights(user) {
    Swal.fire({
      title: 'Are you sure want to change admin status?',
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, change it!'
    }).then((result) => {
      if (result.value) {
        user.isAdmin = !user.isAdmin;
        console.log(user);
        this.userService.update(user).subscribe(_user => {
          Toast.fire({
            type: 'success',
            title: 'User updated!'
          });
          location.reload();
        },
          err => {
            Toast.fire({
              type: 'error',
              title: 'Mistake!',
              text: err
            });
          });
      }
    });
  }
  removeUser(id: any) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You won\'t be able to revert this!',
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.value) {
        this.userService.delete(id)
          .subscribe(
            data => {
              Toast.fire({
                type: 'success',
                title: 'Deleted successfully'
              });
              location.reload();
            },
            err => {
              Toast.fire({
                type: 'error',
                title: 'Please, try again'
              });
            });
      }
    });
  }
}
