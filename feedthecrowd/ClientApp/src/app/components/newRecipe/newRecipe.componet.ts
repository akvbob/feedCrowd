import { Component } from '@angular/core';
import { Router, ActivatedRoute} from '../../../../node_modules/@angular/router';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { RecipeService } from '../../../services/recipe/recipe.service';
import { ProductService } from '../../../services/product/product.service';
import { Recipe } from '../../models/recipe';
import { AlertService } from '../../../services/alert/alert.service';
import { Product } from '../../models/product';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import Swal from 'sweetalert2';
const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 2500
});
@Component({
    selector: 'app-newRecipe',
    templateUrl: './newRecipe.component.html',
    styleUrls: ['./newRecipe.component.css']
})
export class NewRecipeComponent {

    newInfo: FormGroup;
    recipe: Recipe = <Recipe>{};
    selectedPho = null;
    products: Product[];
    localUserId: '';

    TestingMyRULE: '';
    constructor(private ROUTE: ActivatedRoute,
        private router: Router,
        private recipeService: RecipeService,
        private productService: ProductService,
        private formBuilder: FormBuilder,
        private alertService: AlertService,
        iconRegistry: MatIconRegistry,
        sanitizer: DomSanitizer) {
        iconRegistry.addSvgIcon(
            'thumbs-up',
            sanitizer.bypassSecurityTrustResourceUrl('assets/add.png'));
    }

    ngOnInit() {
        this.recipe = null;
        if (sessionStorage.getItem('currentUser')) {
            const localUser = JSON.parse(sessionStorage.getItem('currentUser'));
            this.localUserId = localUser.id;
        }
        this.newInfo = this.formBuilder.group({
            title: ['', Validators.required],
            description: ['', Validators.required],
            time: ['', Validators.required],
            servings: ['', Validators.required],
            image: ['', Validators.required],
            products: this.formBuilder.array([
                this.addProductToForm()
            ])
        });
    }

    addProductToForm(): FormGroup {
        return this.formBuilder.group({
            quantity: ['', Validators.required],
            name: ['', Validators.required]
        });
    }

    addProductButtonClick(): void {
        (<FormArray>this.newInfo.get('products')).push(this.addProductToForm());
    }

    DeleteProduct(index) {
        const control = <FormArray>this.newInfo.controls.products;
        control.removeAt(index);
    }

    get f() { return this.newInfo.controls; }
    onSubmit() {
console.log(this.newInfo.controls.products.value);
        if (this.newInfo.controls.title.value.toString() === '') {
            Toast.fire({
                type: 'error',
                title: 'Enter recipe title!',
                text: 'Please try again'
            });
            return;
        }
        if (this.newInfo.controls.servings.value.toString() === '' || this.newInfo.controls.servings.value < 1) {
            Toast.fire({
                type: 'error',
                title: 'Enter correct number of servings!',
                text: 'Please try again'
            });
            return;
        }
        if(this.newInfo.controls.products.value.length === 0 ||
            this.newInfo.controls.products.value.length === 1 && this.newInfo.controls.products.value[0].name.toString() === ''){
                Toast.fire({
                    type: 'error',
                    title: 'Choose at least 1 product!',
                    text: 'Please try again'
                });
                return;
        }
        if(!this.checkQuantityLegal(this.newInfo.controls.products.value)){
                Toast.fire({
                    type: 'error',
                    title: 'Illegal Product quantity!',
                    text: 'Please try again'
                });
                return;
        }
        if(this.newInfo.controls.description.value.toString() === ''){
            Toast.fire({
                type: 'error',
                title: 'Fill out the description!',
                text: 'Please try again'
            });
            return;
        }
        this.recipeService.create({
            id: 0,
            description: this.newInfo.controls.description.value,
            fkUser: this.localUserId,
            title: this.newInfo.controls.title.value,
            image: this.selectedPho,
            products: this.newInfo.controls.products.value,
            servings: this.newInfo.controls.servings.value,
            time: this.newInfo.controls.time.value
        })
            .subscribe(
                data => {
                    Toast.fire({
                        type: 'success',
                        title: 'Recipe created successfully'
                    });
                    this.router.navigate(['/']);
                },
                error => {
                    Toast.fire({
                        type: 'error',
                        title: 'Recipe was not created',
                        text: 'Check if form is filled in'
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
    checkQuantityLegal(products: Product[]){
        let Iftrue= true;
        products.forEach(o =>{
            if(o.quantity < 0){
                Iftrue = false;
            }
        })
        return Iftrue;
    }
    get formData(){
        return <FormArray>this.newInfo.get('products');
    }
}
