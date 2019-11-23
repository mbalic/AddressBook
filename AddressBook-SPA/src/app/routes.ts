import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ContactListComponent } from './contacts/contact-list/contact-list.component';
import { ContactListResolver } from './_resolvers/contact-list.resolver';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        children: [
            {
                path: 'contacts',
                component: ContactListComponent,
                resolve: { contacts: ContactListResolver }
            }
            // {
            //     path: 'contact/:id',
            //     component: ContactDetailsComponent
            // },
            // {
            //     path: 'contact/edit',
            //     component: MemberEditComponent,
            //     resolve: { user: MemberEditResolver },
            //     canDeactivate: [PreventUnsavedChanges]
            // }
        ]
    },

    // this has to be in the end as wildcard
    // if this is on top it would match every request
    { path: '**', redirectTo: '', pathMatch: 'full' }

];