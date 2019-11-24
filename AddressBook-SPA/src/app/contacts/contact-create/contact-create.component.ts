import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Contact } from 'src/app/_models/contact';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ContactService } from 'src/app/_services/contact.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-contact-create',
  templateUrl: './contact-create.component.html',
  styleUrls: ['./contact-create.component.css']
})
export class ContactCreateComponent implements OnInit {
  // Accessing form component which is in template
  @ViewChild('editForm', { static: true }) editForm: NgForm;
  contact: Contact;
  bsConfig: Partial<BsDatepickerConfig>; // Partial class makes all type props optional

  // Prevents closing browser window if form dirty
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private router: Router,
    private alertify: AlertifyService,
    private contactService: ContactService
  ) { }

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-blue'
    };
  }

  createContact() {
    this.contactService
      .createContact(this.contact)
      .subscribe(
        next => {
          this.alertify.success('Contact created successfully');
          this.editForm.reset(this.contact);
          this.router.navigate(['/contacts']);
        },
        error => {
          this.alertify.error(error);
        }
      );
  }

}
