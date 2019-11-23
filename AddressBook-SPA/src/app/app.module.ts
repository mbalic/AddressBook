// node_modules components
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
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
import { ContactListResolver } from './_resolvers/contact-list.resolver';
import { ContactDetailsComponent } from './contacts/contact-details/contact-details.component';
import { ContactDetailsResolver } from './_resolvers/contact-details.resolver';
import { ContactEditComponent } from './contacts/contact-edit/contact-edit.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    ContactListComponent,
    ContactDetailsComponent,
    ContactEditComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    PaginationModule.forRoot(),
    RouterModule.forRoot(appRoutes),
  ],
  providers: [
    ErrorInterceptorProvider,
    AlertifyService,
    ContactService,
    ContactListResolver,
    ContactDetailsResolver
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
