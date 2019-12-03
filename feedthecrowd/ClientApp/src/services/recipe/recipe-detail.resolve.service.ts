import { Injectable } from '@angular/core';
import {
    Router, Resolve,
    ActivatedRouteSnapshot
} from '@angular/router';
import { Observable } from '../../../node_modules/rxjs';
import { RecipeService } from './recipe.service';

@Injectable()
export class RecipeDetailResolve implements Resolve<any> {
    constructor(private productService: RecipeService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Promise<any> {
        const id = route.params['id'];
        return this.productService.getRecipe(id).toPromise().then(recipe => {
            if (recipe) {
                return recipe;
            } else { // id not found
                this.router.navigate(['/']);
                return false;
            }
        });
    }
}
