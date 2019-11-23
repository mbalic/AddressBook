// node_modules components
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { PaginationModule } from 'ngx-bootstrap';
import { FormsModule } from '@angular/forms';

// local components
import { appRoutes } from './routes';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ContactListComponent } from './contacts/contact-list/contact-list.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { ContactService } from './_services/contact.service';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    ContactListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    PaginationModule.forRoot(),
    RouterModule.forRoot(appRoutes),


  ],
  providers: [
    ErrorInterceptorProvider,
    AlertifyService,
    ContactService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
