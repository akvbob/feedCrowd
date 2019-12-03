import { Component } from '@angular/core';
import { Recipe } from '../../models/recipe';
import { RecipeService } from '../../../services/recipe/recipe.service';
import { Router, ActivatedRoute } from '../../../../node_modules/@angular/router';
import { map, filter, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  styleUrls: ['./home.component.css'],
  templateUrl: './home.component.html',
})
export class HomeComponent {
  config: any;
  recipes: Recipe[];
  errorMessage: string;

  constructor(private recipeService: RecipeService, private router: Router, private route: ActivatedRoute) { }
  ngOnInit() {
    this.config = {
      currentPage: 1,
      itemsPerPage: 16
    };
    this.route.queryParamMap.pipe(map(params => params.get('page')))
      .subscribe(page => this.config.currentPage = page);

    this.recipeService.getRecipes().subscribe(recipes => {
      this.recipes = recipes;
    }, error => {
      this.errorMessage = error.message;
    });
  }

  onRecipeRowClick(recipe: Recipe) {
    this.router.navigate(['recipes', recipe.id]);
  }

  pageChange(newPage: number) {
    this.router.navigate([''], { queryParams: { page: newPage } });
  }
}
