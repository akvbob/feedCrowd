import { Injectable } from '@angular/core';
import {
    Router, Resolve,
    ActivatedRouteSnapshot
} from '@angular/router';
import { ProductService } from './product.service';
import { Product } from '../../app/models/product';
import { Observable } from '../../../node_modules/rxjs';

@Injectable()
export class ProductDetailResolve implements Resolve<any> {
    constructor(private productService: ProductService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Promise<any> {
        const id = route.params['id'];
        return this.productService.getProductsByRecipe(id).toPromise().then(products => {
            if (products) {
                return products;
            } else { // id not found
                this.router.navigate(['/']);
                return false;
            }
        });
    }
}
