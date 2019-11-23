import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { Contact } from 'src/app/_models/contact';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ContactService } from 'src/app/_services/contact.service';

@Component({
  selector: 'app-contact-edit',
  templateUrl: './contact-edit.component.html',
  styleUrls: ['./contact-edit.component.css']
})
export class ContactEditComponent implements OnInit {
  // Accessing form component which is in template
  @ViewChild('editForm', { static: true }) editForm: NgForm;
  contact: Contact;

  // Prevents closing browser window if form dirty
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private contactService: ContactService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.contact = data['contact'];
    });
  }

  updateContact() {
    this.contactService
      .updateContact(this.contact)
      .subscribe(
        next => {
          this.alertify.success('Contact updated successfully');
          this.editForm.reset(this.contact);
        },
        error => {
          this.alertify.error(error);
        }
      );
  }

}
