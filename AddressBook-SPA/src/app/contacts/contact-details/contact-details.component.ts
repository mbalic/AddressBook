import { Component, OnInit } from '@angular/core';
import { Contact } from 'src/app/_models/contact';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ContactService } from 'src/app/_services/contact.service';

@Component({
  selector: 'app-contact-details',
  templateUrl: './contact-details.component.html',
  styleUrls: ['./contact-details.component.css']
})
export class ContactDetailsComponent implements OnInit {
  contact: Contact;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private alertify: AlertifyService,
    private contactService: ContactService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.contact = data['contact'];
    });
  }

  deleteContact(id: Number) {
    this.alertify.confirm('Are you sure you want to delete this contact?', () => {
      this.contactService
        .deleteContact(id)
        .subscribe(
          () => {
            this.alertify.success('Contact has been deleted.');
            this.router.navigate(['/contacts']);
          },
          error => {
            this.alertify.error('Failed to delete contact.');
          }
        );
    });
  }

}
