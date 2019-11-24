import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { Contact } from 'src/app/_models/contact';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ContactService } from 'src/app/_services/contact.service';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { PhoneNumber } from 'src/app/_models/phoneNumber';

@Component({
  selector: 'app-contact-edit',
  templateUrl: './contact-edit.component.html',
  styleUrls: ['./contact-edit.component.css']
})
export class ContactEditComponent implements OnInit {
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
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private contactService: ContactService
  ) { }

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-blue'
    };
    this.route.data.subscribe(data => {
      if (data == null) {
        this.contact.name = '';
        this.contact.dateOfBirth = new Date();
        this.contact.address = '';
        this.contact.phoneNumbers = [];
      } else {
        this.contact = data['contact'];
      }
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

  removePhoneNumber(number: string) {
    this.contact.phoneNumbers.splice(this.contact.phoneNumbers.findIndex(p => p.number === number), 1);
    this.editForm.control.markAsDirty();

  }

  addPhoneNumber(newNumber: string) {
    const newPhoneNumber: PhoneNumber = { number: newNumber, countryCode: '000', description: '' };
    this.contact.phoneNumbers.push(newPhoneNumber);
    this.editForm.control.markAsDirty();
  }
}
