import { Injectable } from '@angular/core';
import { Contact } from '../_models/contact';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../_models/pagination';
import { HttpParams, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getContacts(page?, itemsPerPage?): Observable<PaginatedResult<Contact[]>> {
    const paginatedResult: PaginatedResult<Contact[]> = new PaginatedResult<Contact[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http
      .get<Contact[]>(this.baseUrl + 'contacts', { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
  }

  getContact(id): Observable<Contact> {
    return this.http.get<Contact>(this.baseUrl + 'contacts/' + id);
  }

  createContact(contact: Contact) {
    return this.http.post(this.baseUrl + 'contacts/', contact);
  }

  updateContact(contact: Contact) {
    return this.http.put(this.baseUrl + 'contacts/', contact);
  }

  deleteContact(id: Number) {
    return this.http.delete(this.baseUrl + 'contacts/' + id);
  }
}
