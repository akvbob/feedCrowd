import { FormGroup, FormBuilder, Validators, FormArray } from '../../../../node_modules/@angular/forms';
import { Recipe } from '../../models/recipe';
import { Component } from '../../../../node_modules/@angular/core';
import { Router, ActivatedRoute, ParamMap, Params } from '../../../../node_modules/@angular/router';
import { RecipeService } from '../../../services/recipe/recipe.service';
import { MatIconRegistry } from '../../../../node_modules/@angular/material';
import { DomSanitizer } from '../../../../node_modules/@angular/platform-browser';
import { AlertService } from '../../../services/alert/alert.service';
import { ProductService } from '../../../services/product/product.service';
import { Product } from '../../models/product';
import Swal from 'sweetalert2';
import { switchMap } from '../../../../node_modules/rxjs/operators';
const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 2500
});

@Component({
    // tslint:disable-next-line:component-selector
    selector: 'app-editRecipe',
    templateUrl: './editRecipe.component.html',
    styleUrls: ['./editRecipe.component.css']
})
export class EditRecipeComponent {

    newInfo: FormGroup;
    recipe: Recipe = <Recipe>{};
    selectedPho = null;
    products: Product[] = Array<Product>();
    localUserId: '';
    id: any;
    form: FormArray;
    data: Product[] = Array<Product>();

    constructor(private route: ActivatedRoute,
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

    // tslint:disable-next-line:use-life-cycle-interface
    ngOnInit() {
        this.route.data
            .subscribe((data: { recipe: Recipe, products: Product[] }) => {
                this.recipe = data.recipe;
                this.products = data.products;
            });
        if (sessionStorage.getItem('currentUser')) {
            const localUser = JSON.parse(sessionStorage.getItem('currentUser'));
            this.localUserId = localUser.id;
        }
        this.id = this.route.snapshot.params['id'];
        this.newInfo = this.formBuilder.group({
            title: ['', Validators.required],
            description: [, Validators.required],
            time: ['', Validators.required],
            servings: ['', Validators.required],
            image: ['', Validators.required],
            products: this.formBuilder.array([])
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

    deleteProduct(index) {
        const control = <FormArray>this.newInfo.controls.products;
        control.removeAt(index);
    }

    get f() { return this.newInfo.controls; }
    encodeImageFileAsURL(event) {
        const file = event.target.files[0];
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
            this.selectedPho = reader.result;
        };
    }
    deletecurrentProduct(item) {
        const index = this.products.indexOf(item);
        const a = this.products.splice(index, 1);
    }

    putValues() {
        this.products.map(x => {
            const product: Product = {
                name: x.name,
                quantity: x.quantity
            };
            this.data.push(product);
        });
        this.f.products.value.map(x => {
            const product: Product = {
                name: x.name,
                quantity: x.quantity
            };
            this.data.push(product);
        });
        this.recipe.products = this.data;
    }
    checkQuantityLegal(products: Product[]) {
        let iftrue = true;
        products.forEach(o => {
            if (o.quantity < 0) {
                iftrue = false;
            }
        });
        return iftrue;
    }
    save() {

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
        if (this.newInfo.controls.products.value.length === 0 ||
            this.newInfo.controls.products.value.length === 1 && this.newInfo.controls.products.value[0].name.toString() === '') {
            Toast.fire({
                type: 'error',
                title: 'Choose at least 1 product!',
                text: 'Please try again'
            });
            return;
        }
        if (!this.checkQuantityLegal(this.newInfo.controls.products.value)) {
            Toast.fire({
                type: 'error',
                title: 'Illegal Product quantity!',
                text: 'Please try again'
            });
            return;
        }
        if (this.newInfo.controls.description.value.toString() === '') {
            Toast.fire({
                type: 'error',
                title: 'Fill out the description!',
                text: 'Please try again'
            });
            return;
        }
        if (this.f.title.value !== '') {
            this.recipe.title = this.f.title.value;
        }
        if (this.f.description.value !== '') {
            this.recipe.description = this.f.description.value;
        }
        if (this.f.time.value !== '') {
            this.recipe.time = this.f.time.value;
        }
        if (this.f.image.value !== '') {
            this.recipe.image = this.selectedPho;
        }
        if (this.f.servings.value !== '') {
            this.recipe.servings = this.f.servings.value;
        }
        this.putValues();
        this.recipeService.update(this.id, {
            id: 0,
            description: this.recipe.description,
            fkUser: this.localUserId,
            title: this.recipe.title,
            image: this.recipe.image,
            products: this.data,
            servings: this.recipe.servings,
            time: this.recipe.time
        })
            .subscribe(
                data => {
                    Toast.fire({
                        type: 'success',
                        title: 'Recipe updated successfully'
                    });
                    this.router.navigate(['/']);
                },
                error => {
                    Toast.fire({
                        type: 'error',
                        title: 'Recipe was not updated'
                    });
                    // this.loading = false;
                });

    }
    get formData() {
        return <FormArray>this.newInfo.get('products');
    }
}
