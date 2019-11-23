import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Contact } from '../_models/contact';
import { ContactService } from '../_services/contact.service';

@Injectable()
export class ContactEditResolver implements Resolve<Contact> {
  constructor(
    private contactService: ContactService,
    private router: Router,
    private alertify: AlertifyService
  ) { }

  resolve(route: ActivatedRouteSnapshot): Observable<Contact> {
    return this.contactService.getContact(route.params['id']).pipe(
      catchError(error => {
        this.alertify.error('Problem retrieving your data');
        this.router.navigate(['/contacts']);
        return of(null);
      })
    );
  }
}
