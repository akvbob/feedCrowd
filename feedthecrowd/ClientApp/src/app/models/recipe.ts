import {Product} from './product';
export class Recipe {
    id: number;
    title: string;
    time: string;
    image: string;
    description: string;
    servings: number;
    fkUser: string;
    products: Product[];
}
