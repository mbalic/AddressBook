import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Contact } from '../_models/contact';
import { ContactService } from '../_services/contact.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ContactListResolver implements Resolve<Contact[]> {
  pageNumber = 1;
  pageSize = 5;

  constructor(
    private contactService: ContactService,
    private router: Router,
    private alertify: AlertifyService
  ) { }

  resolve(route: ActivatedRouteSnapshot): Observable<Contact[]> {
    return this.contactService.getContacts(this.pageNumber, this.pageSize).pipe(
      catchError(error => {
        this.alertify.error('Problem retrieving data');
        this.router.navigate(['/home']);
        return of(null);
      })
    );
  }
}
