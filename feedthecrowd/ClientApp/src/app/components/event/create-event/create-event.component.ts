import { Component } from '@angular/core';
import { Router, ActivatedRoute, ParamMap, Params } from '../../../../../node_modules/@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EventService } from 'src/services/event/event.service';
import { Event } from '../../../models/event';
import Swal from 'sweetalert2';
const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 2500
});

@Component({
    // tslint:disable-next-line:component-selector
    selector: 'app-create-event',
    templateUrl: './create-event.component.html',
    styleUrls: ['./create-event.component.css']
})
export class CreateEventComponent {
    eventForm: FormGroup;
    localUserId: any;
    submitted = false;
    constructor(private formBuilder: FormBuilder,
        private router: Router,
        private eventService: EventService) { }

    // tslint:disable-next-line:use-life-cycle-interface
    ngOnInit() {
        this.eventForm = this.formBuilder.group({
            title: ['', Validators.required],
            eventStartDate: ['', Validators.required],
            eventEndDate: ['', Validators.required],
            peoplecount: ['', Validators.required]
        });
        if (sessionStorage.getItem('currentUser')) {
            const localUser = JSON.parse(sessionStorage.getItem('currentUser'));
            this.localUserId = localUser.id;
        }
    }

    onSubmit() {
        this.submitted = true;
        // stop here if form is invalid
        /*if (this.eventForm.invalid) {
            Toast.fire({
                type: 'error',
                title: 'Please fill the full form!',
                text: 'Please try again'
            });
            return;
        }*/
        if (this.eventForm.value.title.toString().length === 0) {
            Toast.fire({
                type: 'error',
                title: 'Enter event title!',
                text: 'Please try again'
            });
            return;
        }
        if (this.eventForm.value.peopleCount < 0) {
            Toast.fire({
                type: 'error',
                title: 'Enter non negative number!',
                text: 'Please try again'
            });
            return;
        }
        if (this.eventForm.value.eventEndDate < this.eventForm.value.eventStartDate) {
            Toast.fire({
                type: 'error',
                title: 'Event start date > Event end date!',
                text: 'Please try again'
            });
            return;
        }
        const el = <Event>{};
        el.title = this.eventForm.value.title;
        el.eventStartDate = this.eventForm.value.eventStartDate;
        el.eventEndDate = this.eventForm.value.eventEndDate;
        el.peopleCount = this.eventForm.value.peoplecount;
        el.userId = this.localUserId;
        this.eventService.create(el)
            // .pipe(first())
            .subscribe(
                data => {
                    Toast.fire({
                        type: 'success',
                        title: 'Event created successfully'
                    });
                    this.router.navigate(['profile']);
                },
                error => {
                    Toast.fire({
                        type: 'error',
                        title: 'Unsuccessful event creation',
                        text: 'Please try again'
                    });
                });
    }
    get f() { return this.eventForm.controls; }
}
