import { Component } from '@angular/core';
import { Recipe } from '../../models/recipe';
import { RecipeService } from '../../../services/recipe/recipe.service';
import { Router, ActivatedRoute, ParamMap } from '../../../../node_modules/@angular/router';
import { switchMap } from '../../../../node_modules/rxjs/operators';
import { ProductService } from '../../../services/product/product.service';
import { Product } from '../../models/product';
import { FormGroup, Validators, FormBuilder } from '../../../../node_modules/@angular/forms';
import { CommentService } from 'src/services/comment/comment.service';
import { NewComment } from '../../models/newComment';
import { Event } from 'src/app/models/event';
import { EventService } from 'src/services/event/event.service';
import Swal from 'sweetalert2';
import { User } from 'src/app/models/user';
const Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 2500
});
@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.css']
})
export class RecipeComponent {

  recipe: Recipe;
  products: Product[];
  id: string;
  recipeProducts: Product[];
  amount: any;
  newInfo: FormGroup;
  comments: Comment[];
  newCom: FormGroup;
  myEvents: Event[];
  user: any;
  addTo: FormGroup;
  image: any;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private recipeService: RecipeService,
    private productService: ProductService,
    private eventService: EventService,
    private commentService: CommentService,
    private formBuilder: FormBuilder) { }
  ngOnInit() {
    if (sessionStorage.getItem('currentUser')) {
      const localUser = JSON.parse(sessionStorage.getItem('currentUser'));
      this.user = localUser.id;

      this.eventService.getUserEvents(this.user).subscribe(events => {
        this.myEvents = JSON.parse(JSON.stringify(events));
      });
    }
    this.route.paramMap.pipe( // combines observable functions
      switchMap((params: ParamMap) => { // cancels previous requests
        // emits Product observable when parameter map changes
        this.id = params.get('id'); // gets id parameter from route parameter array
        return this.recipeService.getRecipe(this.id); // returns Observable<Product>
      }
      )).subscribe(recipe => { // subscription to observable
        this.recipe = recipe; // assign product we get from observable to the class' variable
        this.amount = recipe.servings;
      });
    this.productService.getProductsByRecipe(this.id).subscribe(products => {
      this.products = products;
      this.recipeProducts = JSON.parse(JSON.stringify(this.products));
    });
    this.commentService.getCommentsOfRecipe(this.id).subscribe(comments => {
      this.comments = comments;
    });
    this.newInfo = this.formBuilder.group({
      amount: [this.amount]
    });
    this.newCom = this.formBuilder.group({
      textField: ['', Validators.required],
    });
    this.addTo = this.formBuilder.group({
      event: ['', Validators.required],
    });
  }

  calculateProducts() {
    if (this.newInfo.controls.amount.value !== null && this.newInfo.controls.amount.value !== this.amount &&
      this.newInfo.controls.amount.value > 0) {
      this.amount = this.newInfo.controls.amount.value;
    }
    this.recipeProducts = JSON.parse(JSON.stringify(this.products));
    this.recipeProducts.map(x => x.quantity = x.quantity / this.recipe.servings * this.amount);
  }

  isLoggedIn() {
    let localUser = null;
    if (sessionStorage.getItem('currentUser')) {
      localUser = JSON.parse(sessionStorage.getItem('currentUser'));
    }
    if (localUser != null) {
      return true;
    }
    return false;
  }
  postComment() {
    const newC = <NewComment>{};
    if (this.f.textField.value !== '' && this.isLoggedIn()) {
      newC.textField = this.f.textField.value.toString();
      const localUser = JSON.parse(sessionStorage.getItem('currentUser'));
      newC.userid = localUser.id;
      newC.recipeid = String(this.id);
      this.commentService.createComment(newC).subscribe(comment => {
        // this.newCom = comment;
        location.reload();
      });
    }
  }
  get f() {
    return this.newCom.controls;
  }

  addRecipeToEvent() {
    if (this.addTo.invalid) {
      Toast.fire({
        type: 'error',
        title: 'Choose event!'
      });
      return;
    }
    this.eventService.addRecipeToEvent(String(this.id), String(this.addTo.value.event)).subscribe(ad => {
      Toast.fire({
        type: 'success',
        title: 'Added successfully'
      });
    },
      error => {
        Toast.fire({
          type: 'error',
          title: 'Unsuccessful',
          text: 'Please try again'
        });
      });
  }
  removeComment(id: any) {
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
        this.commentService.delete(id)
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
  isAdmin(){
    let u = null;
    if(sessionStorage.getItem('currentUser')){
      u = JSON.parse(sessionStorage.getItem('currentUser'));
      return u.isAdmin;
    }     
    return false;
  }
}
