import { Component, OnInit } from '@angular/core';
import { Contact } from 'src/app/_models/contact';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { ContactService } from 'src/app/_services/contact.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent implements OnInit {
  contacts: Contact[];
  pagination: Pagination;

  constructor(
    private contactService: ContactService,
    private alertify: AlertifyService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.contacts = data['contacts'].result;
      this.pagination = data['contacts'].pagination;
    });

    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl('https://localhost:5001/notify')
      .build();

    connection.start().then(function () {
      console.log('SignalR Connected!');
    }).catch(function (err) {
      return console.error(err.toString());
    });

    connection.on('BroadcastMessage', () => {
      this.loadContacts();
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadContacts();
  }

  loadContacts() {
    this.contactService
      .getContacts(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe(
        (res: PaginatedResult<Contact[]>) => {
          this.contacts = res.result;
          this.pagination = res.pagination;
        },
        error => {
          this.alertify.error(error);
        }
      );
  }
}
