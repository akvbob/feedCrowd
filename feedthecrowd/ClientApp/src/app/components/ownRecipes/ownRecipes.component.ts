import { Component } from '@angular/core';
import { Recipe } from '../../models/recipe';
import { RecipeService } from '../../../services/recipe/recipe.service';
import { Router } from '../../../../node_modules/@angular/router';
import Swal from 'sweetalert2';
const Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 2500
});

@Component({
  selector: 'app-ownRecipes',
  templateUrl: './ownRecipes.component.html',
  styleUrls: ['./ownRecipes.component.css']
})
export class OwnRecipesComponent {

  recipes: Recipe[];
  errorMessage: string;
  localUserId: any;

  constructor(private recipeService: RecipeService, private router: Router) { }

  ngOnInit() {
    if (sessionStorage.getItem('currentUser')) {
      const localUser = JSON.parse(sessionStorage.getItem('currentUser'));
      this.localUserId = localUser.id;
    }
    this.recipeService.getOwnRecipes(this.localUserId).subscribe(recipes => {
      this.recipes = recipes;
    }, error => {
      this.errorMessage = error.message;
    });
  }

  deleteRecipe(recipe: Recipe) {
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
        this.recipeService.delete(String(recipe.id))
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
                title: 'Cannot delete this recipe'
              });
            });
      }
    });
  }
  editRecipe(recipe: Recipe) {
    this.router.navigate(['editRecipe', recipe.id]);
  }
  onRecipeRowClick(recipe: Recipe) {
    this.router.navigate(['recipes', recipe.id]);
  }
  redirectToCreateRecipe() {
    this.router.navigate(['newRecipe']);
  }
}
