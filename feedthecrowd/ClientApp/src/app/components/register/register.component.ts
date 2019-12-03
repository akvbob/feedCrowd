import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../services/user/user.service';
import { AlertService } from '../../../services/alert/alert.service';
import { User } from 'src/app/models/user';
import Swal from 'sweetalert2';
const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 2500
});
@Component({ templateUrl: 'register.component.html' })
export class RegisterComponent implements OnInit {
    registerForm: FormGroup;
    loading = false;
    submitted = false;

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private userService: UserService,
        private alertService: AlertService) { }

    ngOnInit() {
        this.registerForm = this.formBuilder.group({
            name: ['', Validators.required],
            surname: ['', Validators.required],
            username: ['', Validators.required],
            email: ['', Validators.email],
            password: ['', [Validators.required, Validators.minLength(6)]],
            password2: ['', [Validators.required, Validators.minLength(6)]]
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.registerForm.invalid) {
            Toast.fire({
                type: 'error',
                title: 'Fill the full form!'
            });
            return;
        }
        if (this.registerForm.value.password !== this.registerForm.value.password2) {
            Toast.fire({
                type: 'error',
                title: 'Passwords does not match!'
            });
            return;
        }
        this.loading = true;
        const user = <User>{};
        user.name = this.registerForm.value.name;
        user.surname = this.registerForm.value.surname;
        user.email = this.registerForm.value.email;
        user.userName = this.registerForm.value.userName;
        user.password = this.registerForm.value.password;
        this.userService.register(this.registerForm.value)
            .subscribe(
                data => {
                    Toast.fire({
                        type: 'success',
                        title: 'Added successfully'
                    });
                    this.router.navigate(['/login']);
                },
                error => {
                    Toast.fire({
                        type: 'error',
                        title: 'Unsuccessful',
                        text: 'Choose another name'
                    });
                    this.loading = false;
                });
    }
}
