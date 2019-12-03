import { NgModule } from '@angular/core';
import { Routes, RouterModule, CanActivate } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { RecipeComponent } from './components/recipe/recipe.component';
import { ProfileComponent } from './components/profile/profile.component';
import { NewRecipeComponent } from './components/newRecipe/newRecipe.componet';
import { CreateEventComponent } from './components/event/create-event/create-event.component';
import { EventComponent } from './components/event/event/event.component';
import { OwnRecipesComponent } from './components/ownRecipes/ownRecipes.component';
import { AuthGuard } from './guards/auth.guards';
import { EditRecipeComponent } from './components/editRecipe/editRecipe.component';
import { ProductDetailResolve } from '../services/product/product-detail.resolve.service';
import { RecipeDetailResolve } from '../services/recipe/recipe-detail.resolve.service';
import { AdministrationComponent } from './components/administration/administration.component';
import { AdminGuard } from './guards/admin.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'recipes/:id',
    component: RecipeComponent
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'newRecipe',
    component: NewRecipeComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'createEvent',
    component: CreateEventComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'event/:id',
    component: EventComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'ownRecipes',
    component: OwnRecipesComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'editRecipe/:id',
    component: EditRecipeComponent,
    resolve: {
      recipe: RecipeDetailResolve,
      products: ProductDetailResolve
    },
    canActivate: [AuthGuard],
  },
  {
    path: 'administration',
    component: AdministrationComponent,
    canActivate: [AuthGuard, AdminGuard],
  },
  // otherwise redirect to home
/*  {
    path: '**',
    redirectTo: ''
  }*/
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    ProductDetailResolve,
    RecipeDetailResolve
  ]
})
export class AppRoutingModule { }
