import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor';
import {MatIconModule} from '@angular/material/icon';

import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatCardModule} from '@angular/material/card';
import {MatGridListModule} from '@angular/material/grid-list';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import {NgxPaginationModule} from 'ngx-pagination';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { RecipeComponent } from './components/recipe/recipe.component';
import { ProfileComponent } from './components/profile/profile.component';
import { NewRecipeComponent } from './components/newRecipe/newRecipe.componet';
import { CreateEventComponent } from './components/event/create-event/create-event.component';
import { EventComponent } from './components/event/event/event.component';
import { ExcelService } from 'src/services/excel/excel.service';
import { OwnRecipesComponent } from './components/ownRecipes/ownRecipes.component';
import { EditRecipeComponent } from './components/editRecipe/editRecipe.component';
import { AdministrationComponent } from './components/administration/administration.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    RecipeComponent,
    ProfileComponent,
    NewRecipeComponent,
    CreateEventComponent,
    EventComponent,
    OwnRecipesComponent,
    EditRecipeComponent,
    AdministrationComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    FormsModule,
    MatCardModule,
    MatCardModule,
    MatGridListModule,
    MatIconModule,
    Ng2SearchPipeModule,
    NgxPaginationModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ExcelService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
