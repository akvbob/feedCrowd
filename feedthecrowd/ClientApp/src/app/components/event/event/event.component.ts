import { Component } from '@angular/core';
import { Router, ActivatedRoute, ParamMap, Params } from '../../../../../node_modules/@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EventService } from 'src/services/event/event.service';
import { Event } from '../../../models/event';
import { switchMap } from '../../../../../node_modules/rxjs/operators';
import Swal from 'sweetalert2';
import { Recipe } from 'src/app/models/recipe';
import { RecipeService } from 'src/services/recipe/recipe.service';
import { ExcelService } from 'src/services/excel/excel.service';
import { ProductService } from 'src/services/product/product.service';

const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 2500
});

class ProductToExport {
    id: number;
    recipe: string;
    title: string;
    quantity: number;
    constructor(recipeName, productTitle, productQuantity) {
        this.recipe = recipeName;
        this.title = productTitle;
        this.quantity = productQuantity;
    }
}
@Component({
    // tslint:disable-next-line:component-selector
    selector: 'app-event',
    templateUrl: './event.component.html',
    styleUrls: ['./event.component.css']
})
export class EventComponent {

    dataList: Array<ProductToExport>=[];
    event: Event;
    recipes: Recipe[];
    eventForm: FormGroup;
    id: string;
    
    submitted = false;
    localUserId: any;
    constructor(
        private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private router: Router,
        private eventService: EventService,
        private recipeService: RecipeService,
        private excelService: ExcelService,
        private productService: ProductService) { }

    // tslint:disable-next-line:use-life-cycle-interface
    ngOnInit() {
        this.route.paramMap.pipe(
            switchMap((params: ParamMap) => {
                this.id = params.get('id');
                return this.eventService.getEvent(this.id);
            }
            )).subscribe(ev => { // subscription to observable
                this.event = ev;
            });
        this.recipeService.getByEvent(this.id).subscribe(recipes => {
            this.recipes = JSON.parse(JSON.stringify(recipes)); // recipes;
            this.recipes.forEach(obj => {
                this.productService.getProductsByRecipe(obj.id.toString()).subscribe(products => {
                    obj.products = JSON.parse(JSON.stringify(products));
                });
            });
        });
        if (sessionStorage.getItem('currentUser')) {
            const localUser = JSON.parse(sessionStorage.getItem('currentUser'));
            this.localUserId = localUser.id;
        }
        this.eventForm = this.formBuilder.group({
            title: ['', Validators.required],
            eventStartDate: ['', Validators.required],
            eventEndDate: ['', Validators.required],
            peoplecount: ['', Validators.required]
        });
    }

    onSubmit() {        
        this.submitted = true;
    if (this.eventForm.invalid) {
        return;}
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
        this.eventService.update(String(this.event.id), el)
            .subscribe(
                data => {
                    Toast.fire({
                        type: 'success',
                        title: 'Event updated successfully'
                    });
                    this.router.navigate(['event', this.event.id]);
                },
                error => {
                    Toast.fire({
                        type: 'error',
                        title: 'Unsuccessful event update',
                        text: 'Please try again'
                    });
                });
    }
    removeRecipe(r: any) {
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
                this.eventService.removeRecipeFromEvent(r, this.id)
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

    onRecipeRowClick(recipe: Recipe) {
        this.router.navigate(['recipes', recipe.id]);
    }

    exportAsXLSX(): void {
        this.recipes.forEach(obj => {
            obj.products.forEach(o => {
                const d = new ProductToExport(obj.title, o.name, (o.quantity / obj.servings * this.event.peopleCount).toFixed(2));
                this.dataList.push(d);
            });            
        });
        
        this.excelService.exportAsExcelFile(this.dataList, this.event.title + '(' + this.event.peopleCount + ' people)' + '_');
        Toast.fire({
            type: 'success',
            title: 'Data prepared to save!'
        });
    }
    get f() { return this.eventForm.controls; }
}
