import { Recipe } from './recipe';

export class Event {
    id: number;
    userId: number;
    title: string;
    peopleCount:number;
    eventStartDate: Date;
    eventEndDate: Date;
    dateCreated: Date;
    recipes: Recipe[];
}
