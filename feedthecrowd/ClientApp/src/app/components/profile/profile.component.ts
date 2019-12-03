import { Component } from '@angular/core';
import { UserService } from '../../../services/user/user.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EventService } from 'src/services/event/event.service';
import { Event } from '../../models/event';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
const Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 2500
});

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {

  user: any;
  newInfo: FormGroup;
  localUserId: any;
  selectedPho = null;
  events: Event[];
  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private eventService: EventService, private router: Router) { }
  ngOnInit() {
    this.user = null;
    if (sessionStorage.getItem('currentUser')) {
      const localUser = JSON.parse(sessionStorage.getItem('currentUser'));
      this.localUserId = localUser.id;
    }
    this.userService.getById(this.localUserId).subscribe(user => {
      this.user = user;
    });
    this.eventService.getUserEvents(this.localUserId).subscribe(events => {
      this.events = events; console.log(events);
    }, err => {console.log(err)}
    );
    this.newInfo = this.formBuilder.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      email: ['', Validators.required],
      pic: ['', Validators.required],
    });
  }
  get f() {
    return this.newInfo.controls;
  }
  save() {
    if (this.f.name.value !== '') {
      this.user.name = this.f.name.value;
    }
    if (this.f.surname.value !== '') {
      this.user.surname = this.f.surname.value;
    }
    if (this.f.email.value !== '') {
      this.user.email = this.f.email.value;
    }
    if (this.f.pic.value !== '') {
      this.user.photo = this.selectedPho;
    }
    this.user.id = this.localUserId;
    this.userService.update(this.user).subscribe(user => {
      this.user = user;
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
  encodeImageFileAsURL(event) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.selectedPho = reader.result;
    };
  }
  onEditRowClick(ev: Event) {
    this.router.navigate(['event', ev.id]);
  }
  onEventRowClick(event: Event) {
    this.router.navigate(['event', event.id]);
  }

  removeEvent(ev: Event) {
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
        this.eventService.delete(String(ev.id))
          .subscribe(
            data => {
              Toast.fire({
                type: 'success',
                title: 'Deleted successfully'
              });
              location.reload();
            },
            errr => {
              Toast.fire({
                type: 'error',
                title: 'Please, try again'
              });
            });
      }
    });
  }
}
