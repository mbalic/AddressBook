import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ContactListComponent } from './contacts/contact-list/contact-list.component';
import { ContactListResolver } from './_resolvers/contact-list.resolver';
import { ContactDetailsComponent } from './contacts/contact-details/contact-details.component';
import { ContactDetailsResolver } from './_resolvers/contact-details.resolver';
import { ContactEditComponent } from './contacts/contact-edit/contact-edit.component';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { ContactEditResolver } from './_resolvers/contact-edit.resolver';
import { ContactCreateComponent } from './contacts/contact-create/contact-create.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        children: [
            {
                path: 'contacts',
                component: ContactListComponent,
                resolve: { contacts: ContactListResolver },
            },
            {
                path: 'contacts/:id',
                component: ContactDetailsComponent,
                resolve: { contact: ContactDetailsResolver },
            },
            {
                path: 'contacts/edit/:id',
                component: ContactEditComponent,
                resolve: { contact: ContactEditResolver },
                canDeactivate: [PreventUnsavedChanges]
            },
            {
                path: 'contacts/create',
                component: ContactCreateComponent,
                canDeactivate: [PreventUnsavedChanges]
            },
        ]
    },

    // this has to be in the end as wildcard
    // if this is on top it would match every request
    { path: '**', redirectTo: '', pathMatch: 'full' }

];
